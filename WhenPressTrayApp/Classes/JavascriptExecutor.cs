using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using System;
using System.Diagnostics;
using System.IO;
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
					this.configEntry.ScriptFile + "\r\n\r\n" +
					ex.Message,
					"Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
			catch (Exception ex) {
				MessageBox.Show(
					"Error while running script!\r\n\r\n" +
					this.configEntry.ScriptFile + "\r\n\r\n" +
					ex.Message,
					"Error",
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

			this.engine.AddHostType(
				"ProcessWindowStyle",
				typeof(ProcessWindowStyle));

			this.engine.AddHostObject(
				"WhenPress",
				new JavascriptHostObject());
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
}
