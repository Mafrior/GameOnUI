using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

[CustomEditor(typeof(CraftObject))]
public class CraftGUIEditor : Editor
{
    CraftObject craft;

    List<string> variants = new List<string>
    {
        "",
        "Металл",
        "Дерево"
    };

    public override VisualElement CreateInspectorGUI()
    {
        craft = target as CraftObject;

        VisualElement visual = new VisualElement();
        visual = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>
            ("Assets/Scripts/Editors/CraftStyle.uxml").CloneTree();

        VisualElement cases = visual.Query<VisualElement>("Cases").First();
        TextField nameText = visual.Query<VisualElement>("NameText").First() as TextField;
        nameText.RegisterCallback<ChangeEvent<string>>(e =>
        {
            craft.Name = e.newValue;
            EditorUtility.SetDirty(craft);
        });
        nameText.value = craft.Name;

        for (int i = 0; i < craft.cases.Count; i++)
        {
            cases.Add(CreateCase(craft.cases[i]));
        }

        Button addButton = visual.Query<Button>("AddButton").First();
        addButton.clickable.clicked += AddCase;

        Button removeAllButton = visual.Query<Button>("RemoveLastButton").First();
        removeAllButton.clickable.clicked += RemoveLast;

        visual.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editors/CraftUSS.uss"));
        return visual;
    }

    void AddCase()
    {
        Case newCase = CreateInstance<Case>();
        craft.cases.Add(newCase);
        AssetDatabase.AddObjectToAsset(newCase, craft);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    void RemoveLast()
    {
        if (craft.cases.Count == 0) { return; }
        AssetDatabase.RemoveObjectFromAsset(craft.cases[craft.cases.Count - 1]);
        craft.cases.RemoveAt(craft.cases.Count - 1);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        ResetTarget();
    }

    VisualElement CreateCase(Case targetCase)
    {
        VisualElement resultVisual = new VisualElement();
        var visultree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>
            ("Assets/Scripts/Editors/CaseStyle.uxml");
        resultVisual = visultree.CloneTree();

        List<Button> buttons = resultVisual.Query<Button>("button").ToList();
        List<Label> labels = new List<Label>();

        for (int i = 0; i < buttons.Count; i++)
        {
            labels.Add(buttons[i].Query<Label>("Label").First());

            if (targetCase.variant.Count == buttons.Count)
            {
                labels[i].text = targetCase.variant[i];
            }
            else { targetCase.variant.Add(""); }

            int j = i;
            buttons[i].clickable.clicked += () =>
            {
                int textIndex = variants.FindIndex(x => x == labels[j].text);
                labels[j].text = variants[textIndex == variants.Count - 1 ? 0 : textIndex + 1];
                targetCase.variant[j] = variants[textIndex == variants.Count - 1 ? 0 : textIndex + 1];
            };
        }

        resultVisual.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editors/CaseUSS.uss"));
        return resultVisual;
    }
}
