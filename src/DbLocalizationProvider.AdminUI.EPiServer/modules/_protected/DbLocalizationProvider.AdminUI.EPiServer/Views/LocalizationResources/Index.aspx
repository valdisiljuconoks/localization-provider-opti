﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<DbLocalizationProvider.AdminUI.LocalizationResourceViewModel>" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="EPiServer.Framework.Web.Mvc.Html"%>
<%@ Import Namespace="EPiServer.Framework.Web.Resources"%>
<%@ Import Namespace="EPiServer.Shell" %>
<%@ Import Namespace="EPiServer.Shell.Navigation" %>
<%@ Import Namespace="EPiServer.Shell.Navigation.Internal" %>
<%@ Import Namespace="EPiServer" %>
<%@ Import Namespace=" EPiServer.Shell.Web.Mvc.Html"%>
<%@ Import Namespace=" DbLocalizationProvider"%>
<%@ Import Namespace="DbLocalizationProvider.AdminUI" %>
<%@ Assembly Name="EPiServer.Shell.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%: Html.Translate(() => Resources.Header) %></title>

    <%= Html.CssLink(Paths.ToClientResource(typeof(LocalizationResourceViewModel), "ClientResources/bootstrap.min.css"))%>
    <%= Html.CssLink(Paths.ToClientResource(typeof(LocalizationResourceViewModel), "ClientResources/bootstrap-editable.css"))%>
    <%= Html.CssLink(Paths.ToClientResource(typeof(LocalizationResourceViewModel), "ClientResources/jquery.treetable.min.css"))%>
    <%= Html.CssLink(Paths.ToClientResource(typeof(LocalizationResourceViewModel), "ClientResources/jquery.treetable.theme.default.min.css"))%>

    <%= Page.ClientResources("ShellCore") %>
    <%= Page.ClientResources("ShellWidgets") %>
    <%= Page.ClientResources("ShellCoreLightTheme") %>
    <%= Page.ClientResources("ShellWidgetsLightTheme")%>
    <%= Page.ClientResources("Navigation") %>
    <%= Page.ClientResources("DijitWidgets", new[] { ClientResourceType.Style })%>

    <%= Html.CssLink(UriSupport.ResolveUrlFromUIBySettings("App_Themes/Default/Styles/ToolButton.css")) %>
    <%= Html.ScriptResource(UriSupport.ResolveUrlFromUtilBySettings("javascript/episerverscriptmanager.js"))%>
    <%= Html.ScriptResource(UriSupport.ResolveUrlFromUIBySettings("javascript/system.js")) %>
    <%= Html.ScriptResource(UriSupport.ResolveUrlFromUIBySettings("javascript/dialog.js")) %>
    <%= Html.ScriptResource(UriSupport.ResolveUrlFromUIBySettings("javascript/system.aspx")) %>

    <%= Html.ScriptResource(Paths.ToClientResource(typeof(LocalizationResourceViewModel), "ClientResources/jquery-2.0.3.min.js"))%>
    <%= Html.ScriptResource(Paths.ToClientResource(typeof(LocalizationResourceViewModel), "ClientResources/bootstrap.min.js"))%>
    <%= Html.ScriptResource(Paths.ToClientResource(typeof(LocalizationResourceViewModel), "ClientResources/bootstrap-editable.min.js"))%>
    <%= Html.ScriptResource(Paths.ToClientResource(typeof(LocalizationResourceViewModel), "ClientResources/jquery.tablesorter.min.js"))%>
    <%= Html.ScriptResource(Paths.ToClientResource(typeof(LocalizationResourceViewModel), "ClientResources/jquery.treetable.min.js"))%>

    <style type="text/css">
        html {
            font-size: initial;
        }
        body {
            font-size: 1.2em;
        }

        #resourceList.table-striped tr.leaf td {
            background-color: white;
        }

        table.treetable {
            font-size: 1em;
        }

        table.treetable tbody tr td {
            padding: 8px;
        }

        .table thead:first-child tr:first-child th {
            border: 1px solid #888;
            font-weight: 400;
            padding: .3em 1em .1em;
            text-align: left;
            height: 34px;
            vertical-align: middle;
        }

        table.table > tbody > tr > td {
            height: 30px;
            vertical-align: middle;
        }

        table.table-sorter .header {
            cursor: pointer;
        }

        table.table-sorter thead tr .headerSortDown, table.table-sorter thead tr .headerSortUp {
            background: #bebebe;
            _background: #949494 none;
            color: #ffffff;
            text-shadow: none;
        }

        table.table-sorter thead tr .sortable:after {
            position: relative;
            left: 2px;
            border: 8px solid transparent;
        }

        table.table-sorter thead tr .sortable:after {
            content: '\25ca';
        }

        table.table-sorter thead tr .headerSortDown:after {
            content: '\25b2';
        }

        table.table-sorter thead tr .headerSortUp:after {
            content: '\25bc';
        }

        .headerSortDown,
        .headerSortUp{
            padding-right: 10px;
        }

        .search-input {
            width: 100%;
        }

        .glyphicon {
            font-size: 1.5rem;
            top: -1px;
        }

        .epi-contentContainer {
            max-width: 100%;
            font-size: 62.5%;
        }

        label {
            font-weight: normal;
            margin-top: 5px;
        }

        input[type="radio"], input[type="checkbox"] {
            margin: 0;
        }

        .available-languages {
            margin-bottom: 15px;
        }

        .available-languages-toggle {
            text-decoration: underline;
        }

        a.editable-empty, a.editable-empty:visited {
            color: red;
        }

        a.editable-empty.editable-click, a.editable-click:hover {
            border-bottom-color: red;
        }

        .editable-remove {
            margin-left: 7px;
        }

        .EP-systemMessage {
            display: block;
            border: solid 1px #878787;
            background-color: #fffdbd;
            padding: 0.3em;
            margin-top: 0.5em;
            margin-bottom: 0.5em;
        }

        button.epi-cmsButton-text {
            padding-left: 20px;
            width: auto;
            min-width: 20px;
        }

        button.epi-cmsButton-tools {
            background-repeat: no-repeat;
            background-color: transparent;
            background-position: 1px 1px;
            height: 18px;
            overflow: visible;
            border: 0;
            padding: 0 0 0 20px;
            margin: 0;
            vertical-align: middle;
        }

        .epi-cmsButton-arrowdown:after {
            padding: 0 2px;
            content: '\25bc';
        }

        ul.export-menu {
            background-color: #555b61;
            border: solid 1px #a8a8a8;
            color: #fff;
            margin: 3px 0 0 0;
            padding: 0.3em 0;
            border-radius: 0;
            right: -3px;
            left: auto;
        }

        ul.export-menu li {
            vertical-align: top;
            float: left;
            display: inline-block;
            width: 100%;
            text-align: left;
        }

        ul.export-menu li a, ul.export-menu li a:visited {
            color: #fff;
            font-family: Arial, Helvetica, Sans-Serif;
            font-weight: normal;
            font-size: 13px;
            display: block;
        }

        ul.export-menu li a:hover {
            background: #c4d600;
            color: #333;
        }

        ul.export-menu li a span {
            padding: 0.3em 100px 0.3em 0;
            display: inline-block;
        }

        #exportXliffModal {
            font-size: 62.5%;
        }

        #exportXliffModal .modal-body input {
            margin: 5px;
        }

        #exportXliffModal .modal-body label {
            display: block;
        }
        .btn * {
            font-size: initial;
        }
    </style>
</head>
<body>
    <% if (Model.ShowMenu)
       {
    %><%= @Html.Raw(Html.CreatePlatformNavigationMenu()) %><%
       } %>
    <div <%= @Html.Raw(Html.ApplyFullscreenPlatformNavigation()) %>>
         <div class="epi-contentContainer epi-padding">
        <div class="epi-contentArea epi-paddingHorizontal">
            <h1 class="EP-prefix"><%: Html.Translate(() => Resources.Header) %></h1>
            <div class="epi-paddingVertical">
                <% if (!string.IsNullOrEmpty(ViewData["LocalizationProvider_Message"] as string) || !ViewData.ModelState.IsValid)
                   {
                %>
                <div class="EP-systemMessage">
                    <%= ViewData["LocalizationProvider_Message"] %>
                    <%= Html.ValidationSummary() %>
                </div>
                <%
                   } %>
                <form action="<%= Url.Action("UpdateLanguages") %>" method="post">
                    <div class="available-languages"><a data-toggle="collapse" href="#availableLanguages" aria-expanded="false" aria-controls="availableLanguages" class="available-languages-toggle"><%: Html.Translate(() => Resources.AvailableLanguages) %></a></div>
                    <div class="collapse" id="availableLanguages">
                        <% foreach (var language in Model.Languages)
                           {
                               var isSelected = Model.SelectedLanguages.FirstOrDefault(l => language.Equals(l)) != null;
                        %>
                        <div>
                            <label data-language-code="<%= language.Name %>" data-language-name="<%= language.EnglishName %>">
                                <input type="checkbox" <%= isSelected ? "checked" : string.Empty %> name="languages" value="<%= string.IsNullOrEmpty(language.Name) ? "__invariant" : language.Name %>" /><%= language.EnglishName %>
                            </label>
                        </div>
                        <% } %>
                        <div class="epi-buttonContainer">
                            <span class="epi-cmsButton">
                                <input class="epi-cmsButton-text epi-cmsButton-tools epi-cmsButton-Save" type="submit" id="saveLanguages" value="<%: Html.Translate(() => Resources.Save) %>" title="<%: Html.Translate(() => Resources.Save) %>" /></span>
                        </div>
                    </div>
                    <input type="hidden" name="showMenu" value="<%= Model.ShowMenu %>"/>
                </form>

                <form action="<%= Url.Action("ExportResources") %>" method="get" id="exportForm">
                    <input type="hidden" name="format" id="format" value="json"/>
                </form>
                <form action="<%= Url.Action("ImportResources") %>" method="get" id="importLinkForm">
                    <input type="hidden" name="showMenu" value="<%= Model.ShowMenu %>"/>
                </form>
                <form action="<%= Url.Action("Table") %>" method="get" id="tableViewForm">
                    <input type="hidden" name="showMenu" value="<%= Model.ShowMenu %>"/>
                </form>
                <form action="<%= Url.Action("Tree") %>" method="get" id="treeViewForm">
                    <input type="hidden" name="showMenu" value="<%= Model.ShowMenu %>"/>
                </form>
                <form action="<%= Url.Action("CleanCache") %>" onsubmit="return confirm('<%: Html.Translate(() => Resources.CleanCacheConfirmation) %>')" method="get" id="cleanCacheForm">
                    <input type="hidden" name="showMenu" value="<%= Model.ShowMenu %>"/>
                </form>
                <div class="epi-buttonContainer">
                    <span class="epi-cmsButton">
                    <% if(ConfigurationContext.Current.Export.Providers.Count == 1)
                       { %>
                        <input class="epi-cmsButton-text epi-cmsButton-tools epi-cmsButton-Export" type="submit" id="exportResources" value="<%: Html.Translate(() => Resources.Export) %>" title="<%: Html.Translate(() => Resources.Export) %>" onclick="$('#exportForm').submit();" />
                    <% }
                       else { %>
                        <span class="dropdown">
                            <button class="epi-cmsButton-text epi-cmsButton-tools epi-cmsButton-Export epi-cmsButton-arrowdown dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true"><%: Html.Translate(() => Resources.Export) %></button>
                            <ul class="dropdown-menu export-menu">
                            <% foreach (var provider in ConfigurationContext.Current.Export.Providers)
                               { %>
                                <li><a href="#" id="<%= provider.ProviderId.ToLower() %>-menu-item" data-export-menu-item="<%= provider.ProviderId.ToLower() %>"><span><%= provider.FormatName %></span></a></li>
                            <% } %>
                            </ul>
                        </span>
                       <% } %>
                    </span>
                    <% if (Model.AdminMode)
                       {
                    %>
                        <span class="epi-cmsButton">
                            <input class="epi-cmsButton-text epi-cmsButton-tools epi-cmsButton-Import" type="submit" id="importResources" value="<%: Html.Translate(() => Resources.ImportResources.Import) %>" title="<%: Html.Translate(() => Resources.ImportResources.Import) %>" onclick="$('#importLinkForm').submit();" /></span>
                    <%
                       } %>
                    <% if (Model.AdminMode)
                       {
                    %>
                        <span class="epi-cmsButton">
                            <input class="epi-cmsButton-text epi-cmsButton-tools epi-cmsButton-Delete" type="submit" id="cleanCache" value="<%: Html.Translate(() => Resources.CleanCache) %>" title="<%: Html.Translate(() => Resources.CleanCache) %>" onclick="$('#cleanCacheForm').submit();" /></span>
                    <%
                       } %>
                    <% if (Model.AdminMode)
                       {
                    %>
                        <span class="epi-cmsButton">
                            <input class="epi-cmsButton-text epi-cmsButton-tools epi-cmsButton-AddFile" type="button" id="newResource" value="<%: Html.Translate(() => Resources.New) %>" title="<%: Html.Translate(() => Resources.New) %>" /></span>
                    <%
                       } %>
                </div>
                
                <% if (Model.IsDbSearchEnabled)
                   { %>

                    <form id="resourceQueryFilterForm">
                        <span>
                            <label><%: Html.Translate(() => Resources.TotalRowCount) %> <%: Model.TotalRowCount %></label>
                        </span>
                        <div class="form-group">
                            <input type="search" name="query" value="<%: Model.Query%>"  class="form-control search-input" placeholder="<%: Html.Translate(() => Resources.SearchQueryPlaceholder) %>" />
                        </div>
                    </form>
                <% } %>

                <form id="resourceFilterForm">
                    <div class="form-group">
                        <input type="search" value="" class="form-control search-input" placeholder="<%: Html.Translate(() => Resources.SearchPlaceholder) %>" />
                    </div>
                </form>

                <div class="epi-buttonContainer">
                    <% if (Model.IsTreeViewEnabled && Model.IsTableViewEnabled)
                       { %>
                        <span style="float: left"><label>[&nbsp;
                        <% if (Model.IsTreeView)
                            { %>
                            <%: Html.Translate(() => Resources.TreeView) %>
                        <% } else { %>
                            <a href="#" onclick="javascript: $('#treeViewForm').submit();"><%: Html.Translate(() => Resources.TreeView) %></a>
                        <% } %>
                        &nbsp;|&nbsp;
                        <% if (!Model.IsTreeView)
                            { %>
                            <%: Html.Translate(() => Resources.TableView) %>
                        <% } else { %>
                            <a href="#" onclick="javascript: $('#tableViewForm').submit();"><%: Html.Translate(() => Resources.TableView) %></a>
                        <% } %>
                            &nbsp;]</label></span>
                    <% } %>

                    <span>
                        <input type="checkbox" name="showEmptyResources" id="showEmptyResources"/>
                        <label for="showEmptyResources"><%: Html.Translate(() => Resources.ShowEmpty) %></label>
                    </span>
                    <span>
                        <input type="checkbox" name="showHiddenResources" id="showHiddenResources"/>
                        <label for="showHiddenResources"><%: Html.Translate(() => Resources.ShowHidden) %></label>
                    </span>
                </div>
            <% if (!Model.IsTreeView) { %>
                <table class="table table-bordered table-striped table-sorter" id="resourceList" style="clear: both">
                    <thead>
                        <tr>
                            <th class="sortable"><%: Html.Translate(() => Resources.KeyColumn) %></th>
                            <% foreach (var language in Model.SelectedLanguages)
                               { %>
                            <th class="sortable"><%= language.EnglishName %></th>
                            <% } %>
                            <% if (Model.AdminMode && Model.IsDeleteButtonVisible)
                               {
                            %><th><%: Html.Translate(() => Resources.DeleteColumn) %></th><%
                               }
                               else
                               {
                            %><th class="sortable"><%: Html.Translate(() => Resources.FromCodeColumn) %></th><% } %>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="hidden new-resource-form">
                            <td>
                                <div class="form-inline">
                                    <button class="btn btn-default btn-primary" id="saveResource">
                                        <span href="#" class="glyphicon glyphicon-ok"></span>
                                    </button>
                                    <button class="btn" id="cancelNewResource">
                                        <span href="#" class="glyphicon glyphicon-remove"></span>
                                    </button>
                                    <input class="form-control" id="resourceKey" placeholder="<%: Html.Translate(() => Resources.KeyColumn) %>" style="width: 50%" />
                                </div>
                            </td>
                            <% foreach (var language in Model.SelectedLanguages)
                               { %>
                            <td>
                                <input class="form-control resource-translation" id="<%= (!string.IsNullOrEmpty(language.Name) ? language.Name : "invariant") %>" />
                            </td>
                            <% } %>
                            <% if (Model.AdminMode) { %><td></td><% } %>
                        </tr>

                        <% foreach (var resource in Model.Resources)
                            { %>
                        <tr class="localization resource <%= resource.IsHidden ? "hidden-resource hidden" : "" %>">
                            <td><span title="<%: resource.Key %>"><%: resource.DisplayKey %></span></td>
                            <% foreach (var localizedResource in Model.Resources.Where(r => r.Key == resource.Key))
                                {
                                    foreach (var language in Model.SelectedLanguages)
                                    {
                                        var z = localizedResource.Value.FirstOrDefault(l => l.SourceCulture.Name == language.Name);
                                        if (z != null)
                                        { %>
                            <td>
                                <% if (z.SourceCulture.Name == CultureInfo.InvariantCulture.Name) { %>
                                <%: z.Value %>
                                <% } else { %>
                                <a href="#" id="<%= language.Name %>" data-pk="<%: resource.Key %>" class="translation" data-ismodified="<%= resource.IsModified %>"><%: z.Value %></a>
                                <% } %>
                            </td>
                            <%
                                        }
                                        else
                                        { %>
                            <td>
                                <a href="#" id="<%= language.Name %>" data-pk="<%: resource.Key %>" data-is-empty="True" class="translation"></a>
                            </td>
                                        <% }
                                    }
                                } %>
                            <% if (Model.AdminMode && Model.IsDeleteButtonVisible)
                                {
                               %><td>
                                    <form action="<%= Url.Action("Delete") %>" method="post" class="delete-form">
                                        <input type="hidden" name="pk" value="<%: resource.Key %>"/>
                                        <input type="hidden" name="returnUrl" value="<%= Model.ShowMenu ? Url.Action("Main") : Url.Action("Index") %>" />
                                        <% if (resource.AllowDelete) { %>
                                        <span class="epi-cmsButton">
                                        <%}
                                           else
                                           {
                                        %>
                                            <span class="epi-cmsButtondisabled"><%
                                           } %>
                                        <% if (resource.AllowDelete)
                                           { %>
                                            <input class="epi-cmsButton-tools epi-cmsButton-Delete" type="submit" id="deleteResource" value="" />
                                        <% } %><%
                                           else
                                           { %>
                                            <input class="epi-cmsButton-tools epi-cmsButton-Delete" type="submit" id="deleteResource" value="" disabled="disabled"/>
                                        <% } %>
                                        </span>
                                    </form>
                                </td><%
                                } else { %>
                                <td><%= !resource.AllowDelete %></td>
                                <% } %>
                        </tr>
                        <% } %>
                    </tbody>
                </table>
            <% } %>

            <% if (Model.IsTreeView) { %>
                <div style="text-align: center;">
                    <span><a href="#" onclick="jQuery('#resourceList').treetable('expandAll'); return false;">[+] <%: Html.Translate(() => Resources.ExpandAll) %></a> | <a href="#" onclick="jQuery('#resourceList').treetable('collapseAll'); return false;">[-] <%: Html.Translate(() => Resources.CollapseAll) %></a></span>
                </div>
                <table id="resourceList" class="table table-bordered table-striped table-sorter">
                    <thead>
                        <tr class="header">
                            <th class="header"><%: Html.Translate(() => Resources.KeyColumn) %></th>
                            <% foreach (var language in Model.SelectedLanguages) { %>
                                <th class="header"><%= language.EnglishName %></th>
                            <% } %>
                            <th class="header"><%: Html.Translate(() => Resources.FromCodeColumn) %></th>
                        </tr>
                    </thead>
                    <% foreach (var resource in Model.Tree) { %>
                        <tr data-tt-id="<%= resource.Id %>" <%= resource.ParentId.HasValue ? "data-tt-parent-id=\""+resource.ParentId+"\"" : "" %> class="localization resource <%= resource.IsHidden ? "hidden-resource hidden" : "" %>" data-path="<%: resource.ResourceKey %>">
                            <td style="width: 40%"><%= resource.KeyFragment %></td>
                            <% foreach (var language in Model.SelectedLanguages) {
                                    if(resource.IsLeaf) {
                                        var z = resource.Translations.FirstOrDefault(l => l.SourceCulture.Name == language.Name);
                                        if (z != null) { %>
                                            <td>
                                                <% if (z.SourceCulture.Name == CultureInfo.InvariantCulture.Name) { %>
                                                <%: z.Value %>
                                                <% } else { %>
                                                <a href="#" id="<%= language.Name %>" data-pk="<%: resource.ResourceKey %>" data-ismodified="<%= resource.IsModified %>" class="translation"><%: z.Value %></a>
                                                <% } %>
                                            </td>
                                        <% } else { %>
                                            <td>
                                                <a href="#" id="<%= language.Name %>" data-pk="<%: resource.ResourceKey %>" class="translation"></a>
                                            </td>
                                        <% } %>
                                 <% } else { %>
                                        <td></td>
                                 <% } %>

                            <% } %>
                            <td><%= !resource.AllowDelete %></td>
                        </tr>
                    <% } %>
                </table>
            <%} %>

            <% if(Model.IsTreeView) { %>
                <script type="text/javascript">

                    $(function () {
                        var $table = $('#resourceList');

                        $table.treetable({
                            expandable: true,
                            initialState: <%= UiConfigurationContext.Current.TreeViewExpandedByDefault ? "'expanded'" : "'collapsed'" %> ,
                            clickableNodeNames: true
                        });
                    });

                </script>
            <% } %>
                <script type="text/javascript">

                    var currentResource;

                    function removeTranslation() {
                        var $t = $('a[data-pk=\'' + currentResource.key + '\'][id=\'' + currentResource.lang + '\']');

                        // send remove translation signal to server
                        $.post('<%= Url.Action("Remove") %>', { pk: currentResource.key, name: currentResource.lang }, function() {
                            $t.editable('hide');
                            $t.editable('setValue', '');
                        });
                    }

                    $(function() {
                        $('.localization a.translation').editable({
                            url: '<%= Url.Action("Update") %>',
                            type: 'textarea',
                            placement: 'top',
                            mode: 'popup',
                            title: '<%: Html.Translate(() => Resources.TranslationPopupHeader) %>',
                            emptytext: '<%: Html.Translate(() => Resources.Empty) %>',
                            showbuttons: 'bottom'
                        });

                        $.fn.editableform.buttons = '<button type="submit" class="btn btn-primary btn-sm editable-submit"><i class="glyphicon glyphicon-ok"></i></button>' +
                            '<button type="button" class="btn btn-default btn-sm editable-cancel"><i class="glyphicon glyphicon-remove"></i></button>' +
                            '<button type="button" class="btn btn-danger btn-sm editable-submit editable-remove" onclick="removeTranslation();"><i class="glyphicon glyphicon-trash"></i></button>';

                        var removeButtonDisabled = <%= Model.IsRemoveTranslationButtonDisabled.ToString().ToLower() %>;
                        $('.localization a.translation').on('shown', function (e, editable) {
                            var $t = $(e.currentTarget);
                            currentResource = {
                                key: $t.attr('data-pk'),
                                lang: $t.attr('id')
                            };

                            if ($t.attr('data-ismodified') == "False" || $t.attr('data-is-empty') == "True" || removeButtonDisabled) {
                                $t.siblings('.editable-container').find('.editable-remove').prop('disabled', true);
                            } else {
                                $t.siblings('.editable-container').find('.editable-remove').removeAttr('disabled');
                            }
                        });

                        $('#resourceList').on('submit', '.delete-form', function (e) {
                            e.preventDefault();

                            var $form = $(this);
                            var pk = $(this).find('input[name=pk]').val();
                            if (confirm('<%: Html.Translate(() => Resources.DeleteConfirm) %> `' + pk + '`?')) {
                                $.ajax({ url: $form.attr('action'), method: 'post', data: $form.serialize() });
                                $form.closest('.resource').remove();
                            }
                        });

                        var $filterForm = $('#resourceFilterForm'),
                            $filterInput = $filterForm.find('.form-control:first-child'),
                            $resourceList = $('#resourceList'),
                            $resourceItems = $resourceList.find('.resource'),
                            $showEmpty = $('#showEmptyResources'),
                            $showHidden = $('#showHiddenResources');

                        <% if(!Model.IsTreeView) { %>
                        $resourceList.tablesorter();
                        <% } %>

                        function filter($item, query) {
                            if ($item.length) {
                                if ($item[0].outerHTML.search(new RegExp(query, 'i')) > -1) {
                                    $item.removeClass('hidden');
                                } else {
                                    $item.addClass('hidden');
                                }
                            }
                        }

                        function filterEmpty($item) {
                            if ($item.find('.editable-empty').length == 0) {
                                $item.addClass('hidden');
                            } else {
                                var rk = $item.data('path');
                                $resourceItems.filter('tr[data-path="'+rk+'"]').each(function() { $(this).removeClass('hidden')});
                            }
                        }

                        function runFilter(query) {
                            // clear state
                            $resourceItems.removeClass('hidden');

                            // run search query
                            $resourceItems.each(function() { filter($(this), query); });

                            // filter empty
                            if ($showEmpty.prop('checked')) {
                                $resourceItems.not('.hidden').each(function() { filterEmpty($(this)); });
                            }

                            // filter hidden
                            if (!$showHidden.prop('checked')) {
                                $resourceItems.filter('.hidden-resource').each(function () { $(this).addClass('hidden'); });
                            }
                        }

                        $showEmpty.change(function () {
                            runFilter($filterInput.val());
                        });

                        $showHidden.change(function () {
                            runFilter($filterInput.val());
                        });

                        var t;
                        $filterInput.on('input', function() {
                            clearTimeout(t);
                            t = setTimeout(function() { runFilter($filterInput.val()); }, 500);
                        });

                        $filterForm.on('submit', function (e) {
                            e.preventDefault();
                            clearTimeout(t);
                            runFilter($filterInput.val());
                        });

                        $('#newResource').on('click', function() {
                            $('.new-resource-form').removeClass('hidden');
                            $('#resourceKey').focus();
                        });

                        $('#cancelNewResource').on('click', function() {
                            $('.new-resource-form').addClass('hidden');
                        });

                        $('#saveResource').on('click', function() {
                            var $form = $('.new-resource-form'),
                                $resourceKey = $form.find('#resourceKey').val().replace('+', '%2B');

                            if ($resourceKey.length === 0) {
                                alert("<%: Html.Translate(() => Resources.ResourceKeyRequired) %>");
                                return;
                            }

                            // create object to post to the server - by adding all non-empty translations
                            var $translations = $form.find('.resource-translation');
                            var newResourceObj = {
                                key: $resourceKey,
                                translations: []
                            };

                            $.map($translations, function(el) {
                                if(el.value.length !== 0) {
                                    // language = el.id
                                    // translation = el.value
                                    newResourceObj.translations.push({ language: el.id, value: el.value });
                                }
                            });

                            if (newResourceObj.translations.length === 0) {
                                alert("<%: Html.Translate(() => Resources.TranslationRequired) %>");
                                return;
                            }

                            $.ajax({
                                url: '<%= Url.Action("Create") %>',
                                type: 'POST',
                                dataType: 'json',
                                contentType: 'application/json; charset=utf-8',
                                data: JSON.stringify(newResourceObj)
                            }).success(function() {
                                setTimeout(function() { location.reload(); }, 1000);
                            }).error(function(e) {
                                alert('Error: ' + e.responseJSON.Message);
                            });
                        });
                    });
                </script>
                <script type="text/javascript">
                    $(function() {
                        var $form = $('#exportForm');

                        $('[data-export-menu-item]').click(function() {
                            var $menuItem = $(this);
                            $form.find('#format').val($menuItem.data('export-menu-item'));
                            $form.submit();
                        });

                        $('#exportXliffModal').on('show.bs.modal', function(e) {
                            var $modal = $(e.target),
                                $targetLanguages = $modal.find('.target-languages');

                            $modal.find('.source-languages input:first').attr('checked', 'checked');

                            $targetLanguages.find('input:first').attr('checked', 'checked');
                            $targetLanguages.find('input:eq(1)').attr('checked', 'checked');
                        });

                        $('.export-menu #xliff-menu-item').off('click').on('click', function() {
                            $('#exportXliffModal').modal();
                        });

                        $('#exportButton').click(function() {
                            var $modal = $('#exportXliffModal');

                            $modal.modal('hide');
                            $form.find('#format').val('xliff');

                            if ($form.find('#sourceLang').length != 0)
                                $form.find('#sourceLang').val($modal.find('.source-languages input:checked').val());
                            else
                                $form.append('<input type="hidden" name="sourceLang" id="sourceLang" value="' + $modal.find('.source-languages input:checked').val() + '">');

                            if ($form.find('#targetLang').length != 0)
                                $form.find('#targetLang').val($modal.find('.target-languages input:checked').val());
                            else
                                $form.append('<input type="hidden" name="targetLang" id="targetLang" value="' + $modal.find('.target-languages input:checked').val() + '">');

                            $form.submit();
                        });
                    })
                </script>
            </div>
        </div>
    </div>
    </div>

    <!-- Modal -->
    <div class="modal epi-contentArea" id="exportXliffModal" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><%: Html.Translate(() => Resources.ChooseLanguage) %></h4>
                </div>
                <div class="modal-body row">
                    <form>
                        <fieldset class="col-xs-6 source-languages">
                            <legend><%: Html.Translate(() => Resources.ImportResources.SourceLanguage) %></legend>
                            <% foreach (var sourceLanguage in Model.Languages.Where(l => l != CultureInfo.InvariantCulture))
                                { %>
                                <label><input type="radio" name="sourceLang" value="<%= sourceLanguage.Name %>"/><%= sourceLanguage.EnglishName %><br/></label>
                            <% } %>
                        </fieldset>
                        <fieldset class="col-xs-6 target-languages">
                            <legend><%: Html.Translate(() => Resources.ImportResources.TargetLanguage) %></legend>
                            <% foreach (var sourceLanguage in Model.Languages.Where(l => l != CultureInfo.InvariantCulture))
                                { %>
                                <label><input type="radio" name="targetLang" value="<%= sourceLanguage.Name %>"/><%= sourceLanguage.EnglishName %><br/></label>
                            <% } %>
                        </fieldset>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" id="exportButton" class="btn btn-primary"><%: Html.Translate(() => Resources.Export) %></button>
                    <button type="button" class="btn btn-link" data-dismiss="modal"><%: Html.Translate(() => Resources.Close) %></button>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
