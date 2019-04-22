using System;
using System.Windows.Forms;
using System.Drawing;

namespace VoiceController.Messages
{
	//[ToolboxBitmapAttribute(typeof(ListViewNF), "images.ListViewNF.ico")]
	public sealed class RichTextBoxTS : RichTextBox
	{
		private int scrollto, pos, len;// caret_position, selected_line;

		public RichTextBoxTS()
		{
			try
			{
				SetStyle(ControlStyles.SupportsTransparentBackColor, true);
				//this.text_change = true;
				ColoringMethod = ColoringMethods.No_Coloring;
				DetectUrls = false;
				Dock = DockStyle.Fill;
				AcceptsTab = true;
				SetTabSize();
			}
			catch
			{
				Dispose(true);
			}
		}

		[System.ComponentModel.Description("Mode of the coloring.")]
		public ColoringMethods ColoringMethod { get; set; }

		public bool ScroolToLastLine { get; set; }

		public string GetTextThreadSafe()
		{
			try
			{
				return !InvokeRequired ? Text : (string)Invoke(new StringResultVoidParams(GetTextThreadSafe));
			}
			catch
			{
				return null;
			}
		}
		
		public void SetTextThreadSafe(string text)
		{
			try
			{
                if (!InvokeRequired)
                {
                    Text = text;
                }
                else
                {
                    Invoke(new VoidResultStringParams(SetTextThreadSafe), text);
                }
			}
			catch { }
		}

		public void AppendTextThreadSafe(string text)
		{
			try
			{
                if (!InvokeRequired)
                {
                    AppendText(text);
                }
                else
                {
                    Invoke(new VoidResultStringParams(AppendTextThreadSafe), text);
                }
			}
			catch { }
		}

		public void AppendTextThreadSafe(string text, Color text_color)
		{
			try
			{
                if (!InvokeRequired)
                {
                    int selectionStart = TextLength;
                    AppendText(text);
                    int selectionEnd = TextLength;
                    Select(selectionStart, selectionEnd - selectionStart);
                    SelectionColor = text_color;
                    SelectionLength = 0;
                }
                else
                {
                    Invoke(new VoidResultStringParams(AppendTextThreadSafe), text);
                }
			}
			catch { }
		}

		private void SetTabSize()
		{
			using (var gr = CreateGraphics())
			{
				var preferred_size = gr.MeasureString("M", Font).ToSize();
				var size = preferred_size.Width;
				WinAPI.SendMessage(Handle, (int)WindowMessages.EM_SETTABSTOPS, 1, new [] { size });
			}
		}

		public void BeginUpdate()
		{
			WinAPI.SendMessage(Handle, 0x000B, 0, new int[] { });
		}

		public void EndUpdate()
		{
			WinAPI.SendMessage(Handle, 0x000B, 1, new int[] { });
			Invalidate();
		}

		public void StartUpdate(ref int scrollto, ref int pos, ref int len)
		{
			BeginUpdate();
			scrollto = GetCharIndexFromPosition(new Point(1, 1));
			pos = SelectionStart;
			len = SelectionLength;
		}

		public void StopUpdate(int scrollto, int pos, int len)
		{
			SelectionStart = scrollto;
			ScrollToCaret();
			SelectionStart = pos;
			SelectionLength = len;
			//Select(this.SelectionStart, 0);
			EndUpdate();
		}

		private void SetSelectionBackColor(int line_index, Color color)
		{
			int length = 0;
			int fi = GetFirstCharIndexFromLine(line_index);
			if (fi > Constants.NOT_FOUND)
			{
				while ((Text[fi] != Constants.CARRIAGE_RETURN_CHAR) && (Text[fi] != Constants.LINE_FEED_CHAR))
				{
					length++;
					fi++;
                    if (fi == Text.Length)
                    {
                        break;
                    }
				}

				if (length > 0)
				{
					Select(fi - length, length);
					//text_change = false;
					SelectionBackColor = color;
					//text_change = true;
				}
			}
		}

		// FIXME ha StartUpdate, StopUpdate meghívódik sikertelen a színezés
		protected override void OnTextChanged(EventArgs e)
		{
			if (ScroolToLastLine)
			{
                if (TextLength != 0)
                {
                    Select(TextLength - 1, TextLength - 1);
                }
				ScrollToCaret();
			}
			else
			//if (this.text_change)
			{
				StartUpdate(ref scrollto, ref pos, ref len);

                if (ColoringMethod != ColoringMethods.No_Coloring)
                {
                    Coloring();
                }
				base.OnTextChanged(e);
				StopUpdate(scrollto, pos, len);
			}
		}

		public void ClearColoring()
		{
			SelectAll();
			SelectionFont = Font;
			SelectionColor = ForeColor;
		}

		public void ClearBackgroundColoring()
		{
			Select(0, Text.Length);
			SelectionBackColor = BackColor;
		}

		private void Coloring()
		{
			bool match_case = true;

			string[] c_reserved_words = { "auto", "break", "case", "char", "const", "continue", "default", "do", "double", "else", "enum", "extern", "float", "for", "goto", "if", "int", "long", "register", "return", "short", "signed", "sizeof", "static", "struct", "switch", "typedef", "union", "unsigned", "void", "volatile", "while" };
			string[] cpp_reserved_words = { "asm", "bool", "catch", "class", "const_cast", "delete", "dynamic_cast", "explicit", "false", "friend", "inline", "mutable", "namespace", "new", "operator", "private", "public", "protected", "reinterpret_cast", "static_cast", "template", "this", "throw", "true", "try", "typeid", "typename", "using", "virtual", "wchar_t", "auto", "break", "case", "char", "const", "continue", "default", "do", "double", "else", "enum", "extern", "float", "for", "goto", "if", "int", "long", "register", "return", "short", "signed", "sizeof", "static", "struct", "switch", "typedef", "union", "unsigned", "void", "volatile", "while" };
			string[] predefined_identifiers = { "cin", "cout", "endl", "include", "INT_MIN", "INT_MAX", "iomanip", "iostream", "main", "MAX_RAND", "npos", "NULL", "std", "string" };
			string[] pascal_reserved_words = { "absolute", "and", "array", "asm", "begin", "case", "const", "constructor", "destructor", "div", "do", "downto", "else", "end", "file", "for", "function", "goto", "if", "implementation", "in", "inherited", "inline", "interface", "label", "mod", "nil", "not", "object", "of", "on", "operator", "or", "packed", "procedure", "program", "record", "reintroduce", "repeat", "self", "set", "shl", "shr", "string", "then", "to", "type", "unit", "until", "uses", "var", "while", "with", "xor" };
			string[] c_sharp_reserved_words = { "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const", "const_cast", "reinterpret_cast", "continue", "decimal", "default", "delegate", "DllImport", "dynamic_cast", "static_cast", "do", "double", "else", "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override", "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while" };
			string[] java_reserved_words = { "abstract", "boolean", "break", "byte", "case", "catch", "char", "class", "const", "continue", "default", "do", "double", "else", "extends", "false", "final", "finally", "float", "for", "goto", "assert", "if", "implements", "import", "instanceof", "int", "interface", "long", "native", "new", "null", "package", "private", "protected", "public", "return", "short", "static", "strictfp", "super", "switch", "syncronized", "this", "throw", "throws", "transient", "true", "try", "void", "volatile", "while" };
			string[] c_preprocessor_directives = { "#if", "#else", "#elif", "#endif", "#define", "#undef", "#warning", "#error", "#line", "#region", "#endregion", "#import", "#include", "#using", "#ifdef", "#ifndef", "#pragma" };
			string[] visual_basic_reserved_words = { "AddHandler", "AddressOf", "Alias", "And", "AndAlso", "Ansi", "Append", "As", "Assembly", "Auto", "Binary", "Boolean", "ByRef", "Byte", "ByVal", "Call", "Case", "Catch", "CBool", "CByte", "CChar", "CDate", "CDec", "CDbl", "Char", "CInt", "Class", "CLng", "CObj", "Compare", "CShort", "CSng", "CStr", "CType", "Date", "Decimal", "Declare", "Default", "Delegate", "Dim", "DllImport", "Do", "Double", "Each", "Else", "ElseIf", "End", "EndIf", "Enum", "Erase", "Error", "Event", "Explicit", "False", "For", "Finally", "Friend", "Function", "Get", "GetType", "GoTo", "Handles", "If", "Implements", "Imports", "In", "Inherits", "Input", "Integer", "Interface", "Is", "Let", "Lib", "Like", "Lock", "Long", "Loop", "Me", "Mid", "Mod", "Module", "MustInherit", "MustOverride", "MyBase", "MyClass", "Namespace", "New", "Next", "Not", "Nothing", "NotInheritable", "NotOverridable", "Object", "Off", "On", "Option", "Optional", "Or", "OrElse", "Output", "Overloads", "Overridable", "Overrides", "ParamArray", "Preserve", "Private", "Property", "Protected", "Public", "RaiseEvent", "Random", "Read", "ReadOnly", "ReDim", "Rem", "RemoveHandler", "Resume", "Return", "Seek", "Select", "Set", "Shadows", "Shared", "Short", "Single", "Static", "Step", "Stop", "String", "Structure", "Sub", "SyncLock", "Text", "Then", "Throw", "To", "True", "Try", "TypeOf", "Unicode", "Until", "Variant", "When", "While", "With", "WithEvents", "WriteOnly", "Xor" };

			string[] object_pascal_controls = { "TMainMenu", "TPopupMenu", "TButton", "TLabel", "TEdit", "TMemo", "TToggleBox", "TCheckBox", "TRadioButton", "TListBox", "TComboBox", "TScroolBar", "TGroupBox", "TRadioGroup", "TCheckGroup", "TPanel", "TFrame", "TActionList", "TBitBtn", "TSpeedButton", "TStaticText", "TImage", "TShape", "TBevel", "TPaintBox", "TNotebook", "TLabeledEdit", "TSplitter", "TTrayIcon", "TMaskEdit", "TCheckListBox", "TScrollBox", "TApplicationProperties", "TstringGrid", "TDrawGrid", "TPairSplitter", "TColorBox", "TColorListBox", "TTrackBar", "TProgressBar", "TTreeView", "TListView", "TStatusBar", "TToolBar", "TUpDown", "TPageControl", "TTabControl", "TheaderControl", "TImageList", "TPopupNotifier", "TOpenDialog", "TSaveDialog", "TSelectDirectoryDialog", "TColorDialog", "TFontDialog", "TFindDialog", "TReplaceDialog", "TOpenPictureDialog", "TSavePictureDialog", "TCalendarDialog", "TCalculatorDialog", "TPrinterSetupDialog", "TPrintDialog", "TPageSetupDialog", "TColorButton", "TSpinEdit", "TFloatSpinEdit", "TArrow", "TCalendar", "TEditButton", "TFileNameEdit", "TDirectoyEdit", "TDateEdit", "TCalcEdit", "TFileListBox", "TFilterComboBox", "TXMLPropStorage", "TIniPropStorage", "TBarChart", "TButtonPanel", "TShellTreeView", "TShellListView", "TIDEDialogLayoutStorage", "TDBNavigator", "TDBText", "TDBEdit", "TDBMemo", "TDBImage", "TDBListBox", "TDBLookupListBox", "TDBComboBox", "TDBLookupComboBox", "TDBCheckBox", "TDBRadioGroup", "TDBCalendar", "TDBGroupBox", "TDBGrid", "TDatasource", "TMemDataset", "TSdfDataset", "TFixedFormatDataSet", "TDbf", "TTimer", "TIdleTimer", "TLazComponentQueue", "THTMLHelpDatabase", "THTMLBrowserHelpViewer", "TAsyncProcess", "TProcessUTF8", "TProcess", "TSimpleIPCClient", "TSimpleIPCServer", "TXMLConfig", "TEventLog", "TSynEdit", "TSynAutoComplete", "TSynExporterHTML", "TSynMacroRecorder", "TSynMemo", "TSynPassSyn", "TSynFreePascalSyn", "TSynCppSyn", "TSynJavaSyn", "TSynPerlSyn", "TSynHTMLSyn", "TSynXMLSyn", "TSynLFMSyn", "TSynUNIXShellScriptSyn", "TSynCssSyn", "TSynPHPSyn", "TSynTeXSyn", "TSynSQLSyn", "TSynPythonSyn", "TSynVBSyn", "TSynAnySyn", "TSynMultiSyn", "TTIEdit", "TTIComboBox", "TTIButton", "TTICheckBox", "TTILabel", "TTIGroupBox", "TTIRadioGroup", "TTICheckGroup", "TTICheckListBox", "TTIListBox", "TTIMemo", "TTICalendar", "TTIImage", "TTIFloatSpinEdit", "TTISpinEditTrackBarProgressBar", "TTIMaskEdit", "TTIColorButton", "TMultiPropertyLink", "TTIPropertyGrid", "TTIGrid", "TIpFileDataProvider", "TIpHtmlPanel", "TChart", "TListChartSource", "TRandomChartSource", "TUserDefinedChartSource", "TDbChartSource", "TSQLQuery", "TSQLTransaction", "TSQLScript", "TSQLConnector", "TPQConnection", "TOracleConnection", "TODBCConnection", "TMySQL40Connection", "TMySQL41Connection", "TSQLite3Connection", "TMySQL50Connection", "TIBConnection" };
			string[] object_pascal_reserved_words = { "absolute", "and", "array", "asm", "begin", "case", "const", "constructor", "destructor", "div", "do", "downto", "else", "end", "file", "for", "function", "goto", "if", "implementation", "in", "inherited", "inline", "interface", "label", "mod", "nil", "not", "object", "of", "on", "operator", "or", "packed", "procedure", "program", "record", "reintroduce", "repeat", "self", "set", "shl", "shr", "string", "then", "to", "type", "unit", "until", "uses", "var", "while", "with", "xor", "as", "class", "dispinterface", "except", "exports", "finalization", "finally", "initialization", "inline", "is", "library", "on", "out", "property", "raise", "resourcestring", "threadvar", "try" };

			string[] cli_controls = { "Control", "Form", "MessageBox", "BackgroundWorker", "BindingNavigator", "BindingSource", "Button", "CheckBox", "CheckedListBox", "ColorDialog", "ComboBox", "ContextMenuStrip", "DataGridView", "DataSet", "DateTimePicker", "DirectoryEntry", "DirectorySearcher", "DomainUpDown", "ErrorProvider", "EventLog", "FileSystemWatcher", "FlowLayoutPanel", "FolderBrowserDialog", "FontDialog", "GroupBox", "HelpProvider", "HScroolBar", "ImageList", "Label", "LinkLabel", "ListBox", "ListView", "MaskedTextBox", "MenuStrip", "MessageQueue", "MonthCalendar", "NotifyIcon", "NumericUpDown", "OpenFileDialog", "PageSetupDialog", "Panel", "PerformanceCounter", "PictureBox", "PrintDialog", "PrintDocument", "PrintPreviewControl", "PrintPreviewDialog", "Process", "ProgressBar", "PropertyGrid", "RadioButton", "RichTextBox", "SaveFileDialog", "SerialPort", "ServiceController", "SplitContainer", "Splitter", "StatusStrip", "TabControl", "TableLayoutPanel", "TextBox", "Timer", "ToolStrip", "ToolStripContainer", "ToolTip", "TrackBar", "TreeView", "VScroolBar", "WebBrowser" };
			string[] cpp_cli_reserved_words = { "array", "asm", "auto", "bool", "break", "case", "catch", "char", "class", "const", "const_cast", "continue", "default", "delegate", "delete", "DllImport", "dynamic_cast", "static_cast", "do", "double", "dynamic_cast", "else", "enum", "explicit", "export", "extern", "false", "finally", "float", "for", "for each", "friend", "gcnew", "goto", "if", "inline", "int", "literal", "long", "mutable", "namespace", "new", "nullptr", "operator", "pin_ptr", "private", "protected", "public", "register", "reinterpret_cast", "restrict", "return", "safe_cast", "short", "signed", "sizeof", "static", "static_cast", "struct", "switch", "template", "this", "throw", "true", "try", "typedef", "typeid", "typename", "_typeof", "union", "unsigned", "using", "virtual", "void", "volatile", "wchar_t", "while", "ref", "__super", "override", "event", "initonly" };
			string[] cli_types = { "Object", "Boolean", "Char", "SByte", "Byte", "Int16", "UInt16", "Int32", "UInt32", "Int64", "UInt64", "Single", "Double", "Decimal", "DateTime", "string", "__int8", "__int16", "__int32", "__int64", "Thread", "File", "StringBuilder", "StreamReader", "StreamWriter", "IntPtr", "String" };
			string[] windows_data_types = { "ATOM", "BOOL", "BOOLEAN", "BYTE", "CALLBACK", "CHAR", "COLORREF", "CONST", "DWORD", "DWORDLONG", "DWORD_PTR", "DWORD32", "DWORD64", "FLOAT", "HACCEL", "HALF_PTR", "HANDLE", "HBITMAP", "HBRUSH", "HCOLORSPACE", "HCONV", "HCONVLIST", "HCURSOR", "HDC", "HDDEDATA", "HDESK", "HDROP", "HDWP", "HENHMETAFILE", "HFILE", "HFONT", "HGDIOBJ", "HGLOBAL", "HHOOK", "HICON", "HINSTANCE", "HKEY", "HKL", "HLOCAL", "HMENU", "HMETAFILE", "HMODULE", "HMONITOR", "HPALETTE", "HPEN", "HRESULT", "HRGN", "HRSRC", "HSZ", "HWINSTA", "HWND", "INT", "INT_PTR", "INT32", "INT64", "LANGID", "LCID", "LCTYPE", "LGRPID", "LONG", "LONGLONG", "LONG_PTR", "LONG32", "LONG64", "LPARAM", "LPBOOL", "LPBYTE", "LPCOLORREF", "LPCSTR", "LPCTSTR", "LPCVOID", "LPCWSTR", "LPDWORD", "LPHANDLE", "LPINT", "LPLONG", "LPSTR", "LPTSTR", "LPVOID", "LPWORD", "LPWSTR", "LRESULT", "PBOOL", "PBOOLEAN", "PBYTE", "PCHAR", "PCSTR", "PCTSTR", "PCWSTR", "PDWORD", "PDWORDLONG", "PDWORD_PTR", "PDWORD32", "PDWORD64", "PFLOAT", "PHALF_PTR", "PHANDLE", "PHKEY", "PINT", "PINT_PTR", "PINT32", "PINT64", "PLONG", "PLONGLONG", "PLONG_PTR", "PLONG32", "PLONG64", "POINTER_32", "POINTER_64", "POINTER_SIGNED", "POINTER_UNSIGNED", "PSHORT", "PSIZE_T", "PSSIZE_T", "PSTR", "PTBYTE", "PTCHAR", "PTSTR", "PUCHAR", "PUHALF_PTR", "PUINT", "PUINT_PTR", "PUINT32", "PUINT64", "PULONG", "PULONGLONG", "PULONG_PTR", "PULONG32", "PULONG64", "PUSHORT", "PVOID", "PWCHAR", "PWORD", "PWSTR", "SC_HANDLE", "SC_LOCK", "SERVICE_STATUS_HANDLE", "SHORT", "SIZE_T", "SSIZE_T", "TBYTE", "TCHAR", "UCHAR", "UHALF_PTR", "UINT", "UINT_PTR", "UINT32", "UINT64", "ULONG", "ULONGLONG", "ULONG_PTR", "ULONG32", "ULONG64", "UNICODE_STRING", "USHORT", "USN", "VOID", "WCHAR", "WINAPI", "WORD", "WPARAM" };
			string[] cli_namespaces = { "Activation", "API", "Application", "Assemblies", "Assert", "Binary", "Channels", "CodeAnalysis", "CodeDom", "Collections", "Com2Interop", "Common", "Compiler", "CompilerServices", "ComponentModel", "Compression", "ComTypes", "Configuration", "ConstrainedExecution", "Contexts", "CSharp", "Data", "Deployment", "Design", "Diagnostics", "Drawing", "Drawing2D", "Formatters", "Forms", "Generic", "Hosting", "Install", "Internal", "InteropServices", "Imaging", "IO", "IsolatedStorage", "Isolation", "Lifetime", "Media", "Messaging", "Metadata", "Microsoft", "ObjectModel", "Odbc", "OleDb", "Ports", "Printing", "PropertyGridInternal", "ProviderBase", "Proxies", "Remoting", "Runtime", "SafeHandles", "Serialization", "Server", "Services", "Specialized", "Sql", "SqlClient", "SqlServer", "SqlTypes", "Sziltech", "SymbolStore", "System", "Threading", "Text", "Timers", "Versioning", "VisualBasic", "W3cXsd2001", "Win32", "Windows" };

			string[] c_namespaces = { "std" };

			string[] reserved_words = null, preprocessor_directives = null, types = null, namespaces = null, controls = null;

			ClearColoring();
			switch (ColoringMethod)
			{
				case ColoringMethods.CPP_CLI: // C++/CLI
					reserved_words = cpp_cli_reserved_words;
					preprocessor_directives = c_preprocessor_directives;
					types = cli_types;
					namespaces = cli_namespaces;
					controls = cli_controls;
					break;
				case ColoringMethods.C_Sharp: // C#
					reserved_words = c_sharp_reserved_words;
					preprocessor_directives = c_preprocessor_directives;
					types = cli_types;
					namespaces = cli_namespaces;
					controls = cli_controls;
					break;
				case ColoringMethods.Java: // Java
					reserved_words = java_reserved_words;
					break;
				case ColoringMethods.Object_Pascal: // Object Pascal
					reserved_words = object_pascal_reserved_words;
					match_case = false;
					break;
				case ColoringMethods.Visual_Basic: // Visual Basic
					types = cli_types;
					namespaces = cli_namespaces;
					controls = cli_controls;
					reserved_words = visual_basic_reserved_words;
					match_case = false;
					break;
				case ColoringMethods.Visual_CPP: // Visual C++
					reserved_words = cpp_reserved_words;
					namespaces = c_namespaces;
					reserved_words = cpp_reserved_words;
					preprocessor_directives = c_preprocessor_directives;
					break;
			}

			//ColorWithstring("<", ">", gcnew Font(TAHOMA, FONT_SIZE, FontStyle.Italic), Color.DarkSlateBlue);
			ColorWithArray(reserved_words, new Font(Font.FontFamily, Font.Size, FontStyle.Bold), Color.Blue, match_case);
			ColorWithArray(controls, new Font(Font.FontFamily, Font.Size, FontStyle.Bold), Color.DarkGreen, match_case);

			ColorWithArray(namespaces, new Font(Font.FontFamily, Font.Size, FontStyle.Bold), Color.DarkBlue, match_case);
			ColorWithArray(types, new Font(Font.FontFamily, Font.Size, FontStyle.Bold), Color.DarkBlue, match_case);
			ColorWithArray(windows_data_types, new Font(Font.FontFamily, Font.Size, FontStyle.Bold), Color.DarkBlue, match_case);
			ColorWithArray(preprocessor_directives, new Font(Font.FontFamily, Font.Size, FontStyle.Bold), Color.Purple, Color.DarkRed, match_case);

			//String and character
			ColorWithString("\"", "\"", new Font(Font.FontFamily, Font.Size, FontStyle.Regular), Color.DarkRed);
			if (this.ColoringMethod != ColoringMethods.Visual_Basic)
				ColorWithString("'", "'", new Font(Font.FontFamily, Font.Size, FontStyle.Regular), Color.SteelBlue);

			// Comments
			switch (this.ColoringMethod)
			{
				case ColoringMethods.Object_Pascal:
					ColorWithString("{", "}", new Font(Font.FontFamily, Font.Size, FontStyle.Italic), Color.Green);
					break;
				case ColoringMethods.Visual_Basic:
					ColorWithString("'", null, new Font(Font.FontFamily, Font.Size, FontStyle.Italic), Color.Green);
					break;
				default:
					ColorWithString("//", null, new Font(Font.FontFamily, Font.Size, FontStyle.Italic), Color.Green);
					ColorWithString("/*", "*/", new Font(Font.FontFamily, Font.Size, FontStyle.Italic), Color.Green);
					break;
			}
		}

		private void ColorWithArray(string[] keywords, Font keyword_font, Color color, bool match_case)
		{
			RichTextBoxFinds fm;
			if (match_case) fm = RichTextBoxFinds.MatchCase;
			else fm = RichTextBoxFinds.None;

			if (keywords != null)
			{
				for (var i = 0; i < keywords.Length; i++)
				{
					int index = Constants.NOT_FOUND;
					while ((index = Find(keywords[i], index + 1, fm)) != Constants.NOT_FOUND)
					{
						if ((index == 0) || ((index > 0) && (!Char.IsLetterOrDigit(Text[index - 1]))))
						{
							if ((Text.Length == index + keywords[i].Length) || ((Text.Length > index + keywords[i].Length) && (!Char.IsLetterOrDigit(this.Text[index + keywords[i].Length]))))
							{
								Select(index, keywords[i].Length);
								SelectionFont = keyword_font;
								SelectionColor = color;
							}
						}
					}
				}
			}
		}

		private void ColorWithArray(string[] keywords, Font keyword_font, Color color, Color color2, bool match_case)
		{
			RichTextBoxFinds fm;
			if (match_case) fm = RichTextBoxFinds.MatchCase;
			else fm = RichTextBoxFinds.None;

			if (keywords != null)
			{
				for (int i = 0; i < keywords.Length; i++)
				{
					int index = Constants.NOT_FOUND;
					while ((index = Find(keywords[i], index + 1, fm)) != Constants.NOT_FOUND)
					{
						if ((index == 0) || ((index > 0) && (!Char.IsLetterOrDigit(Text[index - 1]))))
						{
							if ((Text.Length == index + keywords[i].Length) || ((Text.Length > index + keywords[i].Length) && (!Char.IsLetterOrDigit(this.Text[index + keywords[i].Length]))))
							{
								Select(index, keywords[i].Length);
								SelectionFont = keyword_font;
								SelectionColor = color;

								int nli = Text.IndexOf(Constants.LINE_FEED, index + keywords[i].Length);
                                if (nli > Constants.NOT_FOUND)
                                {
                                    Select(index + keywords[i].Length, nli - index - keywords[i].Length);
                                }
                                else
                                {
                                    Select(index + keywords[i].Length, Text.Length - index - keywords[i].Length);
                                }
								SelectionFont = keyword_font;
								SelectionColor = color2;
							}
						}
					}
				}
			}
		}

		public void Coloring(int start_index, int length, Font font, Color forecolor)
		{
			Select(start_index, length);
			SelectionFont = font;
			SelectionColor = forecolor;
			SelectionBackColor = BackColor;
		}

		public void Coloring(int start_index, int length, Font font, Color forecolor, Color backcolor)
		{
			Select(start_index, length);
			SelectionFont = font;
			SelectionColor = forecolor;
			SelectionBackColor = backcolor;
		}

		private void ColorWithString(string beginner_pattern, string ending_patter, Font font, Color color)
		{
            int startIndex, end_index = Constants.NOT_FOUND;

			do
			{
				startIndex = end_index + 1;
				startIndex = Text.IndexOf(beginner_pattern, startIndex);

				if (startIndex != Constants.NOT_FOUND)
				{
                    if (ending_patter != null)
                    {
                        end_index = Text.IndexOf(ending_patter, startIndex + 1);
                    }
                    else
                    {
                        end_index = Text.IndexOf(Constants.LINE_FEED, startIndex + 1);
                    }
                    if (end_index == Constants.NOT_FOUND)
                    {
                        end_index = Text.Length;
                    }

                    if (ending_patter != null)
                    {
                        Select(startIndex, end_index - startIndex + ending_patter.Length);
                    }
                    else
                    {
                        Select(startIndex, end_index - startIndex);
                    }

					SelectionFont = font;
					SelectionColor = color;
				}
			} while ((startIndex != Constants.NOT_FOUND) && (end_index + 1 < Text.Length));
		}
	}
}
