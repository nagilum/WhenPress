using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WhenPressTrayApp {
	public partial class fmTray : Form {
		readonly KeyboardHook keyboardHook = new KeyboardHook();
		private List<ConfigEntry> entries;

		public fmTray() {
			InitializeComponent();
		}

		private void fmTray_Load(object sender, EventArgs e) {
			// Attach the key-pressed event handler.
			this.keyboardHook.KeyPressed += this.keyboardHook_KeyPressed;

			// Remove all traces of config, reload config and re-apply.
			this.loadConfig();
		}

		private void keyboardHook_KeyPressed(object sender, KeyPressedEventArgs e) {
			var entry = null as ConfigEntry;

			foreach (var temp in this.entries.Where(temp => temp.Modifier == e.Modifier && temp.Key == e.Key))
				entry = temp;

			if (entry == null)
				return;

			MessageBox.Show(entry.ScriptFile);
		}

		private void miExecuteScript_Click(object sender, EventArgs e) {
			var menuItem = sender as WhenPressMenuItem;

			if (menuItem == null)
				return;

			MessageBox.Show(menuItem.ConfigEntry.ScriptFile);
		}

		private void miAbout_Click(object sender, EventArgs e) {
			MessageBox.Show(
				Application.ProductName + "\r\n" +
				Application.ProductVersion,
				"About",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);
		}

		private void miReloadConfig_Click(object sender, EventArgs e) {
			// Remove all traces of config, reload config and re-apply.
			this.loadConfig();
		}

		private void miExit_Click(object sender, EventArgs e) {
			// Remove tray-icon.
			this.niTray.Dispose();
			
			// Terminate application.
			Application.Exit();
		}

		/// <summary>
		/// Remove all traces of config, reload config and re-apply.
		/// </summary>
		private void loadConfig() {
			// Load and parse the JSON file.
			var filename = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 4) + ".json";

			try {
				var temp = new JavaScriptSerializer().Deserialize<List<ConfigEntry>>(File.ReadAllText(filename));
				this.entries = temp;
			}
			catch (Exception ex) {
				MessageBox.Show(
					"You done fucked up son!\r\n\r\n" +
					"Unable to correctly parse the application config JSON file: " + filename + "\r\n\r\n" +
					"Thrown error was: " + ex.Message,
					"Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);

				return;
			}

			// Remove all script-submenu-items.
			this.miScripts.DropDownItems.Clear();

			// Set the main script parent menu-item as hidden.
			this.miScripts.Visible = false;
			this.miSeparatorScripts.Visible = false;

			// Unregister all hotkeys.
			this.keyboardHook.UnregisterHotKeys();

			// Cycle parsed JSON file.
			foreach (var entry in this.entries) {
				// Assign hotkey.
				if (entry.Key > 0)
					this.keyboardHook.RegisterHotKey(
						entry.Modifier,
						entry.Key);

				// Create menu item.
				if (!string.IsNullOrWhiteSpace(entry.TrayMenuText)) {
					var menuItem = new WhenPressMenuItem {
						ConfigEntry = entry,
						Text = entry.TrayMenuText
					};

					menuItem.Click += this.miExecuteScript_Click;

					this.miScripts.DropDownItems.Add(menuItem);
					this.miScripts.Visible = true;
					this.miSeparatorScripts.Visible = true;
				}
			}
		}
	}
}
