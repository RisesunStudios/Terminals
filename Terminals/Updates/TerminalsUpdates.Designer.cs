﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.832
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591

namespace Terminals.Updates {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.ComponentModel.ToolboxItem(true)]
    [System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema")]
    [System.Xml.Serialization.XmlRootAttribute("TerminalsUpdates")]
    [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")]
    public partial class TerminalsUpdates : System.Data.DataSet {
        
        private UpdateDataTable tableUpdate;
        
        private System.Data.SchemaSerializationMode _schemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public TerminalsUpdates() {
            this.BeginInit();
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            base.Relations.CollectionChanged += schemaChangedHandler;
            this.EndInit();
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected TerminalsUpdates(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : 
                base(info, context, false) {
            if ((this.IsBinarySerialized(info, context) == true)) {
                this.InitVars(false);
                System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler1 = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
                this.Tables.CollectionChanged += schemaChangedHandler1;
                this.Relations.CollectionChanged += schemaChangedHandler1;
                return;
            }
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((this.DetermineSchemaSerializationMode(info, context) == System.Data.SchemaSerializationMode.IncludeSchema)) {
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["Update"] != null)) {
                    base.Tables.Add(new UpdateDataTable(ds.Tables["Update"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(strSchema)));
            }
            this.GetSerializationData(info, context);
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public UpdateDataTable Update {
            get {
                return this.tableUpdate;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.BrowsableAttribute(true)]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public override System.Data.SchemaSerializationMode SchemaSerializationMode {
            get {
                return this._schemaSerializationMode;
            }
            set {
                this._schemaSerializationMode = value;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new System.Data.DataTableCollection Tables {
            get {
                return base.Tables;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new System.Data.DataRelationCollection Relations {
            get {
                return base.Relations;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void InitializeDerivedDataSet() {
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override System.Data.DataSet Clone() {
            TerminalsUpdates cln = ((TerminalsUpdates)(base.Clone()));
            cln.InitVars();
            cln.SchemaSerializationMode = this.SchemaSerializationMode;
            return cln;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeTables() {
            return false;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void ReadXmlSerializable(System.Xml.XmlReader reader) {
            if ((this.DetermineSchemaSerializationMode(reader) == System.Data.SchemaSerializationMode.IncludeSchema)) {
                this.Reset();
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.ReadXml(reader);
                if ((ds.Tables["Update"] != null)) {
                    base.Tables.Add(new UpdateDataTable(ds.Tables["Update"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXml(reader);
                this.InitVars();
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            this.WriteXmlSchema(new System.Xml.XmlTextWriter(stream, null));
            stream.Position = 0;
            return System.Xml.Schema.XmlSchema.Read(new System.Xml.XmlTextReader(stream), null);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars() {
            this.InitVars(true);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars(bool initTable) {
            this.tableUpdate = ((UpdateDataTable)(base.Tables["Update"]));
            if ((initTable == true)) {
                if ((this.tableUpdate != null)) {
                    this.tableUpdate.InitVars();
                }
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitClass() {
            this.DataSetName = "TerminalsUpdates";
            this.Prefix = "";
            this.EnforceConstraints = true;
            this.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            this.tableUpdate = new UpdateDataTable();
            base.Tables.Add(this.tableUpdate);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private bool ShouldSerializeUpdate() {
            return false;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static System.Xml.Schema.XmlSchemaComplexType GetTypedDataSetSchema(System.Xml.Schema.XmlSchemaSet xs) {
            TerminalsUpdates ds = new TerminalsUpdates();
            System.Xml.Schema.XmlSchemaComplexType type = new System.Xml.Schema.XmlSchemaComplexType();
            System.Xml.Schema.XmlSchemaSequence sequence = new System.Xml.Schema.XmlSchemaSequence();
            xs.Add(ds.GetSchemaSerializable());
            System.Xml.Schema.XmlSchemaAny any = new System.Xml.Schema.XmlSchemaAny();
            any.Namespace = ds.Namespace;
            sequence.Items.Add(any);
            type.Particle = sequence;
            return type;
        }
        
        public delegate void UpdateRowChangeEventHandler(object sender, UpdateRowChangeEvent e);
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        [System.Serializable()]
        [System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class UpdateDataTable : System.Data.DataTable, System.Collections.IEnumerable {
            
            private System.Data.DataColumn columnDescription;
            
            private System.Data.DataColumn columnUrl;
            
            private System.Data.DataColumn columnVersion;
            
            private System.Data.DataColumn columnMD5;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public UpdateDataTable() {
                this.TableName = "Update";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal UpdateDataTable(System.Data.DataTable table) {
                this.TableName = table.TableName;
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected UpdateDataTable(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : 
                    base(info, context) {
                this.InitVars();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn DescriptionColumn {
                get {
                    return this.columnDescription;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn UrlColumn {
                get {
                    return this.columnUrl;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn VersionColumn {
                get {
                    return this.columnVersion;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn MD5Column {
                get {
                    return this.columnMD5;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public UpdateRow this[int index] {
                get {
                    return ((UpdateRow)(this.Rows[index]));
                }
            }
            
            public event UpdateRowChangeEventHandler UpdateRowChanging;
            
            public event UpdateRowChangeEventHandler UpdateRowChanged;
            
            public event UpdateRowChangeEventHandler UpdateRowDeleting;
            
            public event UpdateRowChangeEventHandler UpdateRowDeleted;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void AddUpdateRow(UpdateRow row) {
                this.Rows.Add(row);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public UpdateRow AddUpdateRow(string Description, string Url, string Version, string MD5) {
                UpdateRow rowUpdateRow = ((UpdateRow)(this.NewRow()));
                rowUpdateRow.ItemArray = new object[] {
                        Description,
                        Url,
                        Version,
                        MD5};
                this.Rows.Add(rowUpdateRow);
                return rowUpdateRow;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public virtual System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public override System.Data.DataTable Clone() {
                UpdateDataTable cln = ((UpdateDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Data.DataTable CreateInstance() {
                return new UpdateDataTable();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal void InitVars() {
                this.columnDescription = base.Columns["Description"];
                this.columnUrl = base.Columns["Url"];
                this.columnVersion = base.Columns["Version"];
                this.columnMD5 = base.Columns["MD5"];
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private void InitClass() {
                this.columnDescription = new System.Data.DataColumn("Description", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnDescription);
                this.columnUrl = new System.Data.DataColumn("Url", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnUrl);
                this.columnVersion = new System.Data.DataColumn("Version", typeof(string), null, System.Data.MappingType.Attribute);
                base.Columns.Add(this.columnVersion);
                this.columnMD5 = new System.Data.DataColumn("MD5", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnMD5);
                this.columnVersion.Namespace = "";
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public UpdateRow NewUpdateRow() {
                return ((UpdateRow)(this.NewRow()));
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Data.DataRow NewRowFromBuilder(System.Data.DataRowBuilder builder) {
                return new UpdateRow(builder);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Type GetRowType() {
                return typeof(UpdateRow);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanged(System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.UpdateRowChanged != null)) {
                    this.UpdateRowChanged(this, new UpdateRowChangeEvent(((UpdateRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanging(System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.UpdateRowChanging != null)) {
                    this.UpdateRowChanging(this, new UpdateRowChangeEvent(((UpdateRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleted(System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.UpdateRowDeleted != null)) {
                    this.UpdateRowDeleted(this, new UpdateRowChangeEvent(((UpdateRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleting(System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.UpdateRowDeleting != null)) {
                    this.UpdateRowDeleting(this, new UpdateRowChangeEvent(((UpdateRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void RemoveUpdateRow(UpdateRow row) {
                this.Rows.Remove(row);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public static System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(System.Xml.Schema.XmlSchemaSet xs) {
                System.Xml.Schema.XmlSchemaComplexType type = new System.Xml.Schema.XmlSchemaComplexType();
                System.Xml.Schema.XmlSchemaSequence sequence = new System.Xml.Schema.XmlSchemaSequence();
                TerminalsUpdates ds = new TerminalsUpdates();
                xs.Add(ds.GetSchemaSerializable());
                System.Xml.Schema.XmlSchemaAny any1 = new System.Xml.Schema.XmlSchemaAny();
                any1.Namespace = "http://www.w3.org/2001/XMLSchema";
                any1.MinOccurs = new decimal(0);
                any1.MaxOccurs = decimal.MaxValue;
                any1.ProcessContents = System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any1);
                System.Xml.Schema.XmlSchemaAny any2 = new System.Xml.Schema.XmlSchemaAny();
                any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
                any2.MinOccurs = new decimal(1);
                any2.ProcessContents = System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any2);
                System.Xml.Schema.XmlSchemaAttribute attribute1 = new System.Xml.Schema.XmlSchemaAttribute();
                attribute1.Name = "namespace";
                attribute1.FixedValue = ds.Namespace;
                type.Attributes.Add(attribute1);
                System.Xml.Schema.XmlSchemaAttribute attribute2 = new System.Xml.Schema.XmlSchemaAttribute();
                attribute2.Name = "tableTypeName";
                attribute2.FixedValue = "UpdateDataTable";
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                return type;
            }
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public partial class UpdateRow : System.Data.DataRow {
            
            private UpdateDataTable tableUpdate;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal UpdateRow(System.Data.DataRowBuilder rb) : 
                    base(rb) {
                this.tableUpdate = ((UpdateDataTable)(this.Table));
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string Description {
                get {
                    try {
                        return ((string)(this[this.tableUpdate.DescriptionColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'Description\' in table \'Update\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableUpdate.DescriptionColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string Url {
                get {
                    try {
                        return ((string)(this[this.tableUpdate.UrlColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'Url\' in table \'Update\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableUpdate.UrlColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string Version {
                get {
                    try {
                        return ((string)(this[this.tableUpdate.VersionColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'Version\' in table \'Update\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableUpdate.VersionColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string MD5 {
                get {
                    try {
                        return ((string)(this[this.tableUpdate.MD5Column]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'MD5\' in table \'Update\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableUpdate.MD5Column] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsDescriptionNull() {
                return this.IsNull(this.tableUpdate.DescriptionColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetDescriptionNull() {
                this[this.tableUpdate.DescriptionColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsUrlNull() {
                return this.IsNull(this.tableUpdate.UrlColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetUrlNull() {
                this[this.tableUpdate.UrlColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsVersionNull() {
                return this.IsNull(this.tableUpdate.VersionColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetVersionNull() {
                this[this.tableUpdate.VersionColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsMD5Null() {
                return this.IsNull(this.tableUpdate.MD5Column);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetMD5Null() {
                this[this.tableUpdate.MD5Column] = System.Convert.DBNull;
            }
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class UpdateRowChangeEvent : System.EventArgs {
            
            private UpdateRow eventRow;
            
            private System.Data.DataRowAction eventAction;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public UpdateRowChangeEvent(UpdateRow row, System.Data.DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public UpdateRow Row {
                get {
                    return this.eventRow;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
    }
}

#pragma warning restore 1591