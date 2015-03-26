/**
 * @file
 * Brings a window to the foreground or starts the app if not found.
 *
 * @author
 * Stian Hanger <pdnagilum@gmail.com>
 */

'use strict';

var title     = WhenPress.GetConfigValue('title'),
    fileName  = WhenPress.GetConfigValue('fileName'),
    handle    = null,
    processes = WhenPress.GetProcesses();

processes.forEach(function (process) {
  if (process.MainWindowTitle.indexOf(title) === -1)
    return;

  handle = process.MainWindowHandle;
});

if (handle)
  WhenPress.FocusWindowByHandle(handle);
else
  WhenPress.Start(fileName);
