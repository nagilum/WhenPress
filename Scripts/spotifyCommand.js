/**
 * @file
 * Perform various Spotify command.
 *
 * @author
 * Stian Hanger <pdnagilum@gmail.com>
 */

'use strict';

var command = WhenPress.GetConfigValue('performSpotifyCommand'),
    handle  = null,
    message = null;

WhenPress.GetProcesses().forEach(function (process) {
  if (process.MainWindowTitle.indexOf('Spotify') === -1)
    return;

  handle = process.MainWindowHandle;
});

if (handle) {
  switch (command) {
    case 'previous':
      message = 786432;
      break;

    case 'next':
      message = 720896;
      break;

    case 'playPause':
      message = 917504;
      break;

    case 'stop':
      message = 851968;
      break;

    case 'volumeUp':
      message = 655360;
      break;

    case 'volumeDown':
      message = 589824;
      break;
  }
}

if (message) {
  WhenPress.SendWin32Message(
    handle,
    '0x0319',
    '0x00000000',
    message);
}
