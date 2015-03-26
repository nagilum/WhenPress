using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace WhenPressTrayApp {
	public class JavascriptExecutor {
		private V8ScriptEngine engine;
		private readonly ConfigEntry configEntry;
		private string javascriptSource;

		/// <summary>
		/// Init a new instance of the JS executor!
		/// </summary>
		public JavascriptExecutor(ConfigEntry entry) {
			this.configEntry = entry;

			// Read the source of the given script file to memory.
			this.readScriptSourceFile();

			// Attempt to init a V8 script engine and execite the loaded script.
			if (!string.IsNullOrWhiteSpace(this.javascriptSource))
				this.executeScript();
		}

		/// <summary>
		/// Attempt to init a V8 script engine and execite the loaded script.
		/// </summary>
		private void executeScript() {
			try {
				engine = new V8ScriptEngine();
			}
			catch (Exception ex) {
				MessageBox.Show(
					"Could not init a V8 engine.\r\n\r\n" +
					ex.Message,
					"Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}

			if (engine == null)
				return;

			// Inject all host objects that are to be available to the script.
			this.prepareHostObjects();

			// Execute the loaded script.
			try {
				engine.Execute(this.javascriptSource);
			}
			catch (ScriptEngineException ex) {
				MessageBox.Show(
					"Error while running script!\r\n\r\n" +
					"File:\r\n" +
					this.configEntry.ScriptFile + "\r\n\r\n" +
					"Exception:\r\n" +
					ex.Message + "\r\n\r\n" +
					"Data:\r\n" +
					ex.Data + "\r\n\r\n" +
					"ErrorDetails:\r\n" +
					ex.ErrorDetails + "\r\n\r\n",
					"ScriptEngineException",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
			catch (Exception ex) {
				MessageBox.Show(
					"Error while running script!\r\n\r\n" +
					this.configEntry.ScriptFile + "\r\n\r\n" +
					ex.Message,
					"Exception",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Inject all host objects that are to be available to the script.
		/// </summary>
		private void prepareHostObjects() {
			this.engine.AddHostType(
				"Console",
				typeof(Console));

			this.engine.AddHostType(
				"MessageBoxButtons",
				typeof(MessageBoxButtons));

			this.engine.AddHostType(
				"MessageBoxIcon",
				typeof(MessageBoxIcon));

			this.engine.AddHostType(
				"DialogResult",
				typeof(DialogResult));

			this.engine.AddHostObject(
				"WhenPress",
				new JavascriptHostObject(
					this.configEntry.Parameters,
					this.engine));
		}

		/// <summary>
		/// Read the source of the given script file to memory.
		/// </summary>
		private void readScriptSourceFile() {
			if (!File.Exists(this.configEntry.ScriptFile)) {
				MessageBox.Show(
					"The given script-file does not exits!\r\n\r\n" +
					this.configEntry.ScriptFile,
					"File not found!",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);

				return;
			}

			try {
				this.javascriptSource = File.ReadAllText(configEntry.ScriptFile);
			}
			catch (Exception ex) {
				MessageBox.Show(
					"Could not read the source script-file!\r\n\r\n" +
					configEntry.ScriptFile + "\r\n\r\n" +
					ex.Message,
					"Error Reading File!",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
		}
	}

	/// <summary>
	/// Object available inside the script.
	/// </summary>
	public class JavascriptHostObject {
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

		[DllImport("User32.dll")]
		private static extern IntPtr SetForegroundWindow(IntPtr hWnd);

		/// <summary>
		/// A list of parameters from the config.
		/// </summary>
		private readonly Dictionary<string, string> configParameters;

		/// <summary>
		/// The executing engine.
		/// </summary>
		private readonly V8ScriptEngine engine;

		/// <summary>
		/// Constructor!
		/// </summary>
		public JavascriptHostObject(Dictionary<string, string> configParameters, V8ScriptEngine engine) {
			this.configParameters = configParameters;
			this.engine = engine;
		}

		/// <summary>
		/// Bring a window to the foreground based on the window handle.
		/// </summary>
		public void FocusWindowByHandle(object handle) {
			int tempI;

			if (!int.TryParse(handle.ToString(), out tempI))
				return;

			var tempP = new IntPtr(tempI);

			if (tempP == IntPtr.Zero)
				return;

			SetForegroundWindow(tempP);
		}

		/// <summary>
		/// Bring a window to the foreground based on parts of its main window title.
		/// </summary>
		public void FocusWindowByTitle(string title) {
			foreach (var process in Process.GetProcesses().Where(process => process.MainWindowTitle.ToLower().IndexOf(title.ToLower(), StringComparison.CurrentCulture) != -1))
				SetForegroundWindow(process.MainWindowHandle);
		}

		/// <summary>
		/// Get the handle of the active window.
		/// </summary>
		public IntPtr GetActiveWindowHandle() {
			return GetForegroundWindow();
		}

		/// <summary>
		/// Get the title of the active window.
		/// </summary>
		public string GetActiveWindowTitle() {
			const int chars = 256;
			var handle = GetForegroundWindow();
			var buff = new StringBuilder(chars);

			GetWindowText(handle, buff, chars);

			return buff.ToString();
		}

		/// <summary>
		/// Get a value from the config parameters.
		/// </summary>
		public string GetConfigValue(string name) {
			return (from cf in this.configParameters where cf.Key == name select cf.Value).FirstOrDefault();
		}

		/// <summary>
		/// Get a list of all processes.
		/// </summary>
		public object GetProcesses() {
			return Process.GetProcesses().ToScriptArray(this.engine);
		}

		/// <summary>
		/// Get a list of processes filtered by name.
		/// </summary>
		public object GetProcessesByName(string name) {
			return Process.GetProcessesByName(name).ToScriptArray(this.engine);
		}

		/// <summary>
		/// Send key-strokes and bail out.
		/// </summary>
		public void SendKeyPress(string keys) {
			SendKeys.Send(keys);
		}

		/// <summary>
		/// Send key-strokes and wait for the process to finish.
		/// </summary>
		public void SendKeyPressWait(string keys) {
			SendKeys.SendWait(keys);
		}

		/// <summary>
		/// Display a message-box with just a message.
		/// </summary>
		public DialogResult ShowMessageBox(
			object message) {
			return MessageBox.Show(
				message.ToString());
		}

		/// <summary>
		/// Display a message-box with message and title.
		/// </summary>
		public DialogResult ShowMessageBox(
			object message,
			object title) {
			return MessageBox.Show(
				message.ToString(),
				title.ToString());
		}

		/// <summary>
		/// Display a message-box with message, title, and buttons.
		/// </summary>
		public DialogResult ShowMessageBox(
			object message,
			object title,
			MessageBoxButtons buttons) {
			return MessageBox.Show(
				message.ToString(),
				title.ToString(),
				buttons);
		}

		/// <summary>
		/// Display a message-box with message, title, buttons, and an icon.
		/// </summary>
		public DialogResult ShowMessageBox(
			object message,
			object title,
			MessageBoxButtons buttons,
			MessageBoxIcon icon) {
			return MessageBox.Show(
				message.ToString(),
				title.ToString(),
				buttons,
				icon);
		}

		/// <summary>
		/// Attempt to run a file on the system.
		/// </summary>
		public void Start(string filename) {
			this.Start(
				filename,
				null);
		}

		/// <summary>
		/// Attempt to run a file on the system with arguments.
		/// </summary>
		public void Start(string filename, string arguments) {
			var process = new Process {
				StartInfo = new ProcessStartInfo {
					FileName = filename,
					Arguments = arguments
				}
			};

			process.Start();
		}
	}

	public static class ScriptHelpers {
		public static object ToScriptArray<T>(this IEnumerable<T> source, V8ScriptEngine engine) {
			dynamic array = engine.Evaluate("[]");
			foreach (var element in source) {
				array.push(element);
			}
			return array;
		}
	}
}
