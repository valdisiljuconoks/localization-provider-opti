// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System;
using System.Web.Mvc;
using EPiServer.Shell.ObjectEditing;

namespace DbLocalizationProvider.EPiServer
{
    public class LocalizedEnumAttribute : Attribute, IMetadataAware
    {
        public LocalizedEnumAttribute(Type enumType, bool isManySelection = false)
        {
            EnumType = enumType ?? throw new ArgumentNullException(nameof(enumType));
            IsManySelection = isManySelection;
        }

        public Type EnumType { get; set; }
        public bool IsManySelection { get; }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            if(!(metadata is ExtendedMetadata extendedMetadata))
                return;

            extendedMetadata.ClientEditingClass = "epi-cms/contentediting/editors/" + (IsManySelection ? "CheckBoxListEditor" : "SelectionEditor");
            extendedMetadata.SelectionFactoryType = typeof(LocalizedEnumSelectionFactory<>).MakeGenericType(EnumType);
        }
    }
}
