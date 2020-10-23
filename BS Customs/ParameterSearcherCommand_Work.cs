﻿/* ParameterSearcherCommand_Work.cs
 * https://www.bimtrovert.com
 * © BIMtrovert, 2019
 *
 * This file contains the methods which are used by the 
 * command.
 */
#region Namespaces
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Resources;
using System.Reflection;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using WPF = System.Windows;
using System.Linq;
using Bushman.RevitDevTools;
using BIMtrovert.BS_Customs.Properties;
#endregion


namespace BIMtrovert.BS_Customs
{

    public sealed partial class ParameterSearcherCommand
    {

        private bool DoWork(ExternalCommandData commandData,
            ref String message, ElementSet elements)
        {

            if (null == commandData)
            {

                throw new ArgumentNullException(nameof(
                    commandData));
            }

            if (null == message)
            {

                throw new ArgumentNullException(nameof(message)
                    );
            }

            if (null == elements)
            {

                throw new ArgumentNullException(nameof(elements
                    ));
            }

            ResourceManager res_mng = new ResourceManager(
                  GetType());
            ResourceManager def_res_mng = new ResourceManager(
                typeof(Properties.Resources));

            UIApplication ui_app = commandData.Application;
            UIDocument ui_doc = ui_app?.ActiveUIDocument;
            Application app = ui_app?.Application;
            Document doc = ui_doc?.Document;
            Categories catSet = doc.Settings.Categories;

            var tr_name = res_mng.GetString("_transaction_name"
                );

            try
            {
                using (var tr = new Transaction(doc, tr_name)
                    )
                {

                    if (TransactionStatus.Started == tr.Start()
                        )
                    {



                        ParameterSearcher ps = new ParameterSearcher(catSet, commandData);
                        ps.Show();
                        return TransactionStatus.Committed ==
                            tr.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                /* TODO: Handle the exception here if you need 
                 * or throw the exception again if you need. */
                throw ex;
            }
            finally
            {

                res_mng.ReleaseAllResources();
                def_res_mng.ReleaseAllResources();
            }

            return false;
        }

        
    }
}
