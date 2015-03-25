/**
 * @file
 * Close the active window or tab by sending Ctrl+F4, but only in certain
 * applications.
 *
 * @author
 * Stian Hanger <pdnagilum@gmail.com>
 */

'use strict';

var title = WhenPress.GetActiveWindowTitle(),
    apps  = [
      'Microsoft Visual Studio'
    ];

// Check if the active app is one of the programs we should do this for.
if (apps.some(function (value) { return title.indexOf(value) > -1; })) {
  WhenPress.SendKeyPress("^{F4}");
}
