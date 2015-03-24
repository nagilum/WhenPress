/**
 * @file
 * A test of various dialog boxes.
 *
 * @author
 * Stian Hanger <pdnagilum@gmail.com>
 */

'use strict';

var dialogResult = WhenPress.ShowMessageBox(
  'This is a message box display a test, a title, some buttons, and an icon. ' +
  'The buttons should be "Yes", "No", and "Cancel". ' +
  'Click one of them!',
  'ShowMessageBox',
  MessageBoxButtons.YesNoCancel,
  MessageBoxIcon.Question);

WhenPress.ShowMessageBox(
  dialogResult,
  'You pressed');
