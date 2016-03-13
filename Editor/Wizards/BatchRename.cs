using UnityEditor;
using UnityEngine;

namespace UnityExtensions.Editor.Wizards
{
    public class BatchRename : ScriptableWizard
    {
        //Base name
        public string baseName = "Object ";

        //Start Count
        public int startNumber = 0;

        //Increment
        public int increment = 1;

        [MenuItem("Edit/Batch Rename...")]
        private static void CreateWizard()
        {
            DisplayWizard("Batch Rename", typeof(BatchRename), "Rename");
        }

        //Called when the window first appears
        private void OnEnable()
        {
            UpdateSelectionHelper();
        }

        //Function called when selection changes in scene
        private void OnSelectionChange()
        {
            UpdateSelectionHelper();
        }

        //Update selection counter
        private void UpdateSelectionHelper()
        {
            helpString = "";

            if (Selection.objects != null)
            {
                helpString = "Number of objects selected: " + Selection.objects.Length;
            }
        }

        //Rename
        private void OnWizardCreate()
        {
            //If selection empty, then exit
            if (Selection.objects == null)
                return;

            //Current Increment
            int postFix = startNumber;

            //Cycle and rename
            foreach (Object obj in Selection.objects)
            {
                obj.name = baseName + postFix;
                postFix += increment;
            }
        }
    }
}