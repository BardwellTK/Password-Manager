using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF_Password_Manager.DataTypes;

namespace WPF_Password_Manager
{
    /// <summary>
    /// </summary>
    public partial class MainWindow : Window
    {
      /// <summary>
      /// DecryptDeed: Takes a deed, and performs actions based on deed
      /// </summary>
      private void DecryptDeed(bool undo)
      {
          //undo = undo ? true : false;
          //redo = !undo ? true : false;
          //what event occurred?
          Deed deed = null;
          if (undo)
          {
              deed = _eventHistory.Undo();
          }
          else
          {
              deed = _eventHistory.Redo();
          }

          if (deed != null)
          {
              var eventType = deed.Action;

              //{Delete,Add,Edit,ReTitle,Back,Select} ---------

              //Method ListChange(Deed deed, bool undo)
              //{
              //    IF ADD && REDO >> List.Add
              //    IF DELETE && UNDO >> List.Add
              //    IF DELETE && REDO >> List.Delete
              //    IF ADD && UNDO >> List.Delete
              //}
              if (eventType == EventType.Add || eventType == EventType.Delete)
              {
                  ListChange(deed,undo);
              }

              //Method ApplyData(Deed deed, bool undo)
              //{
              //    //**Special case, sum both into one, because they
              //    //**just overwrite a container in one.
              //    IF EDIT || RETITLE
              //    {
              //        IF UNDO >> Apply.deed.Before;
              //        IF REDO >> Apply.deed.After;
              //    }
              //}
              if (eventType == EventType.Edit || eventType == EventType.ReTitle)
              {
                  ApplyData(deed,undo);
              }

              //Method GoMenu(Deed deed, bool undo)
              //{
              //    IF BACK && UNDO >> Go.Forward
              //    IF SELECT && REDO >> Go.Forward
              //    IF SELECT && UNDO >> Go.Backward
              //    IF BACK && REDO >> Go.Backward
              //}
              if (eventType == EventType.Back || eventType == EventType.Select)
              {
                GoMenu(deed,undo);
              }
          }
        }

      private void ListChange(Deed deed, bool undo)
      {
        var eventType = deed.Action;
        if ((eventType == EventType.Delete && undo) || (eventType == EventType.Add && !undo))
        {
          StaticAdd(deed.Before);
        }
        else if ((eventType == EventType.Add && undo) || (eventType == EventType.Delete && !undo))
        {
          StaticDelete(deed.Before);
        }
      }

      private void ApplyData(Deed deed, bool undo)
      {
        var eventType = deed.Action;
            //Sum edit and re-title into one method? OverwriteContainer(Container c);
            //Because undo always uses deed.Before and vice versa.
          if (eventType == EventType.Edit || eventType == EventType.ReTitle)
          {
              if (undo)
              {
                StaticOverwriteContainer(deed.Before);
                }
              else
              {
                StaticOverwriteContainer(deed.After);
                }
          }

      }

      private void GoMenu(Deed deed, bool undo)
      {
        var eventType = deed.Action;
        if ((eventType == EventType.Back && undo) || (eventType == EventType.Select && !undo))
        {
                if (menuIndex != MenuLocation.Box)
                {
                    //Menu.Go.Forward
                    StaticSelect(deed.Before);
                }
        }
        else if ((eventType == EventType.Select && undo) || (eventType == EventType.Back && !undo))
        {
          if (menuIndex != MenuLocation.Main)
                {
                    //Menu.Go.Backward
                    StaticBack();

                }
        }
      }
    }
}
