/**
 * @file
 * Attempt to start Notepad.
 *
 * @author
 * Stian Hanger <pdnagilum@gmail.com>
 */

'use strict';

// Start an instance of Notepad.
WhenPress.Start('notepad.exe');

// Start an instance of Notepad and attempt to open the file 'test.txt'.
WhenPress.Start('notepad.exe', 'test.txt');
