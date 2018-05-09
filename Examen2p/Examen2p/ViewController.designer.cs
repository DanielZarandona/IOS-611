// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Examen2p
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        UIKit.UITableView tableViewTewwt { get; set; }

        [Action ("BtnIconos:")]
        partial void BtnIconos (Foundation.NSObject sender);
        
        void ReleaseDesignerOutlets ()
        {
            if (tableViewTewwt != null) {
                tableViewTewwt.Dispose ();
                tableViewTewwt = null;
            }
        }
    }
}
