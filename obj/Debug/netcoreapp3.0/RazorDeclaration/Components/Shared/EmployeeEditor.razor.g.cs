#pragma checksum "C:\Users\rynowak\source\repos\ComponentFunTime\Components\Shared\EmployeeEditor.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3a6624dde845a2ddff816749b14fdaf0a1cdcd67"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace ComponentFunTime.Components.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using System.Net.Http;
    using Microsoft.AspNetCore.Components.Layouts;
    using Microsoft.AspNetCore.Components.Routing;
    using Microsoft.JSInterop;
    using ComponentFunTime.Components.Shared;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Components.Forms;
    public class EmployeeEditor : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.RenderTree.RenderTreeBuilder builder)
        {
        }
        #pragma warning restore 1998
#line 68 "C:\Users\rynowak\source\repos\ComponentFunTime\Components\Shared\EmployeeEditor.razor"
            
    EditContext editContext;
    Dictionary<FeatureArea, ValidationMessageStore> stores;

    [Parameter] Person Person { get; set; }
    [Parameter] Action OnCancel { get; set; }
    [Parameter] Action OnConfirm { get; set; }

    protected override void OnInit()
    {
        editContext = new EditContext(Person);
        editContext.AddDataAnnotationsValidation();

        // Validate feature areas too.
        editContext.OnFieldChanged += (sender, e) => ValidateField(e);
        editContext.OnValidationRequested += (sender, e) => ValidateModel();

        stores = new Dictionary<FeatureArea, ValidationMessageStore>();
        for (var i = 0; i < Person.Areas.Count; i++)
        {
            stores.Add(Person.Areas[i], new ValidationMessageStore(editContext));
        }
    }

    void ValidateField(FieldChangedEventArgs e)
    {
        if (e.FieldIdentifier.Model is FeatureArea area && stores.TryGetValue(area, out var store))
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(e.FieldIdentifier.Model)
            {
                MemberName = e.FieldIdentifier.FieldName, // #YOLO
            };

            var propertyValue = area.GetType().GetProperty(e.FieldIdentifier.FieldName).GetValue(area);
            Validator.TryValidateProperty(propertyValue, validationContext, validationResults);

            store.Clear();
            foreach (var validationResult in validationResults)
            {
                foreach (var memberName in validationResult.MemberNames)
                {
                    store.Add(new FieldIdentifier(area, memberName), validationResult.ErrorMessage);
                }
            }

            editContext.NotifyValidationStateChanged();
        }
    }

    void ValidateModel()
    {
        foreach (var kvp in stores)
        {
            var validationContext = new ValidationContext(kvp.Key);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(kvp.Key, validationContext, validationResults, true);

            kvp.Value.Clear();
            foreach (var validationResult in validationResults)
            {
                foreach (var memberName in validationResult.MemberNames)
                {
                    kvp.Value.Add(new FieldIdentifier(kvp.Key, memberName), validationResult.ErrorMessage);
                }
            }
        }

        editContext.NotifyValidationStateChanged();
    }

    void AddArea()
    {
        var area = new FeatureArea();
        Person.Areas.Add(area);
        stores.Add(area, new ValidationMessageStore(editContext));
    }

    void RemoveArea()
    {
        var area = Person.Areas[Person.Areas.Count - 1];
        Person.Areas.RemoveAt(Person.Areas.Count - 1);
        editContext.NotifyValidationStateChanged();
        stores.Remove(area);
    }

    Task OnSubmit()
    {
        if (editContext.Validate())
        {
            OnConfirm();
        }

        return Task.CompletedTask;
    }

#line default
#line hidden
    }
}
#pragma warning restore 1591
