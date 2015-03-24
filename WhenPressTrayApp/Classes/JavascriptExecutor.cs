using System;
using System.IO;
using System.Windows.Forms;

namespace WhenPressTrayApp {
	public class JavascriptExecutor {
		private readonly ConfigEntry configEntry;
		private string javascriptSource;

		/// <summary>
		/// Init a new instance of the JS executor!
		/// </summary>
		public JavascriptExecutor(ConfigEntry entry) {
			this.configEntry = entry;

			// Read the source of the given script file to memory.
			this.readScriptSourceFile();
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
}
