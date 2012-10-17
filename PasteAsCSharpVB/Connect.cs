using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using Microsoft.VisualStudio.CommandBars;
using Extensibility;
using EnvDTE;
using EnvDTE80;
using System.Windows.Forms;


namespace PasteAsCSharpVB
{
	public class Connect : IDTExtensibility2, IDTCommandTarget
	{
		private const vsCommandStatus STATUS_INVISIBLE = vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusInvisible;
		private const vsCommandStatus STATUS_UNSUPPORTED = vsCommandStatus.vsCommandStatusUnsupported;
		private const vsCommandStatus STATUS_DISABLED = vsCommandStatus.vsCommandStatusSupported;
		private const vsCommandStatus STATUS_ENABLED = vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;

		DTE2 _applicationObject;

		AddIn _addInInstance;
		
		///<summary>Implements the constructor for the Add-in object. Place your initialization code within this method.</summary>
		public Connect()
		{
		}

		///<summary>Implements the OnConnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being loaded.</summary>
		///<param name='application'>Root object of the host application.</param>
		///<param name='connectMode'>Describes how the Add-in is being loaded.</param>
		///<param name='addInInst'>Object representing this Add-in.</param>
		///<remarks></remarks>
		public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
		{
			_applicationObject = (DTE2)application;
			_addInInstance = (AddIn)addInInst;

			if (connectMode == ext_ConnectMode.ext_cm_UISetup)
			{
				Commands2 commands = (Commands2)_applicationObject.Commands;
				string editMenuName = null;

				try
				{
					// If you would like to move the command to a different menu, 
					// change the word "edit" to the English version of the menu. 
					// This code will take the culture, append on the name of the 
					// menu then add the command to that menu. You can find a list 
					// of all the top-level menus in the file CommandBar.resx.
					System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("PasteAsCSharpVB.CommandBar", System.Reflection.Assembly.GetExecutingAssembly());

					System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(_applicationObject.LocaleID);
					editMenuName = resourceManager.GetString(string.Concat(cultureInfo.TwoLetterISOLanguageName, "Edit"));

				}
				catch (Exception e)
				{
					//We tried to find a localized version of the word edit, but 
					// one was not found. Default to the en-US word, which may 
					// work for the current culture.
					editMenuName = "Edit";
				}

				//Place the command on the edit menu.
				//Find the MenuBar command bar, which is the top-level command 
				// bar holding all the main menu items:
				CommandBars commandBars = (CommandBars)_applicationObject.CommandBars;
				CommandBar menuBarCommandBar = commandBars["MenuBar"];

				//Find the edit command bar on the MenuBar command bar:
				CommandBarControl editControl = menuBarCommandBar.Controls[editMenuName];
				CommandBarPopup editPopup = (CommandBarPopup)editControl;

				Microsoft.VisualStudio.CommandBars.CommandBar itemToolBar =
					((Microsoft.VisualStudio.CommandBars.CommandBars)_applicationObject.CommandBars)["Code Window"];


				try
				{
					//Add a command to the Commands collection:
					object[] nullArr = null;
					Command command = commands.AddNamedCommand2(_addInInstance, "PasteAsVB", "Paste as Visual Basic",
						"Convert the clipboard from C# to VB.NET and paste", true, 59, ref nullArr,
						(int)(STATUS_ENABLED),
						(int)vsCommandStyle.vsCommandStylePictAndText, vsCommandControlType.vsCommandControlTypeButton);

					//Find the appropriate command bar on the MenuBar command bar:
					command.AddControl(editPopup.CommandBar, 12);
					command.AddControl(itemToolBar, 12);

					command = commands.AddNamedCommand2(_addInInstance, "PasteAsCS", "Paste as C#",
						"Convert the clipboard from VB.NET to C# and paste", true, 59, ref nullArr,
						Convert.ToInt32(STATUS_ENABLED),
						(int)vsCommandStyle.vsCommandStylePictAndText, vsCommandControlType.vsCommandControlTypeButton);

					//Find the appropriate command bar on the MenuBar command bar:
					command.AddControl(editPopup.CommandBar, 12);
					command.AddControl(itemToolBar, 12);
				}
				catch (System.ArgumentException argumentException)
				{
					//  If we are here, then the exception is probably because a 
					//  command with that name already exists. If so there is no 
					//  need to recreate the command and we can safely ignore 
					//  the exception.
				}

			}
		}

		///<summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary>
		///<param name='disconnectMode'>Describes how the Add-in is being unloaded.</param>
		///<param name='custom'>Array of parameters that are host application specific.</param>
		///<remarks></remarks>
		public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
		{
		}

		///<summary>Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification that the collection of Add-ins has changed.</summary>
		///<param name='custom'>Array of parameters that are host application specific.</param>
		///<remarks></remarks>
		public void OnAddInsUpdate(ref Array custom)
		{
		}

		///<summary>Implements the OnStartupComplete method of the IDTExtensibility2 interface. Receives notification that the host application has completed loading.</summary>
		///<param name='custom'>Array of parameters that are host application specific.</param>
		///<remarks></remarks>
		public void OnStartupComplete(ref Array custom)
		{
		}

		///<summary>Implements the OnBeginShutdown method of the IDTExtensibility2 interface. Receives notification that the host application is being unloaded.</summary>
		///<param name='custom'>Array of parameters that are host application specific.</param>
		///<remarks></remarks>
		public void OnBeginShutdown(ref Array custom)
		{
		}

		///<summary>Implements the QueryStatus method of the IDTCommandTarget interface. This is called when the command's availability is updated</summary>
		///<param name='commandName'>The name of the command to determine state for.</param>
		///<param name='neededText'>Text that is needed for the command.</param>
		///<param name='status'>The state of the command in the user interface.</param>
		///<param name='commandText'>Text requested by the neededText parameter.</param>
		///<remarks></remarks>
		public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText)
		{
			if (neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
			{
				Debug.WriteLine(commandName);
				if (commandName == "PasteAsCSharpVB.Connect.PasteAsVB")
				{

					if (_applicationObject.ActiveDocument != null &&
						_applicationObject.ActiveDocument.FullName.ToLower().EndsWith(".vb") &&
						Clipboard.GetText() != string.Empty)
					{
						status = STATUS_ENABLED;
					}
					else
					{
						status = STATUS_DISABLED;
					}
				}
				else if (commandName == "PasteAsCSharpVB.Connect.PasteAsCS")
				{

					if (_applicationObject.ActiveDocument != null &&
						_applicationObject.ActiveDocument.FullName.ToLower().EndsWith(".cs") &&
						Clipboard.GetText() != string.Empty)
					{
						status = STATUS_ENABLED;
					}
					else
					{
						status = STATUS_DISABLED;
					}
				}
				else
				{
					status = STATUS_UNSUPPORTED;
				}
			}
		}

		///<summary>Implements the Exec method of the IDTCommandTarget interface. This is called when the command is invoked.</summary>
		///<param name='commandName'>The name of the command to execute.</param>
		///<param name='executeOption'>Describes how the command should be run.</param>
		///<param name='varIn'>Parameters passed from the caller to the command handler.</param>
		///<param name='varOut'>Parameters passed from the command handler to the caller.</param>
		///<param name='handled'>Informs the caller if the command was handled or not.</param>
		///<remarks></remarks>
		public void Exec(string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled)
		{
			handled = false;
			if (executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault)
			{
				bool nameVB = commandName == "PasteAsCSharpVB.Connect.PasteAsVB";
				bool nameCS = commandName == "PasteAsCSharpVB.Connect.PasteAsCS";
				if (nameCS || nameVB)
				{
					NRefactoryConverter conv = new NRefactoryConverter();
					string result = conv.ConvertCodeSnippet(Clipboard.GetText(), nameVB);
					TextSelection selection = (TextSelection)_applicationObject.ActiveDocument.Selection;
					selection.Insert(result);

					/*ConverterForm f = new ConverterForm();
					if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						TextSelection selection = (TextSelection)_applicationObject.ActiveDocument.Selection;
						selection.Insert(f.VBCode);
						if (f.Reformat)
						{
							_applicationObject.ExecuteCommand("Edit.FormatDocument");
						}
					}*/
					handled = true;
					return;
				}
			}
		}
	}
}