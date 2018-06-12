﻿//------------------------------------------------------------------------------
// <gerado automaticamente>
//     Esse código foi gerado por uma ferramenta.
//     //
//     As alterações no arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </gerado automaticamente>
//------------------------------------------------------------------------------

namespace WSCreateProjectDiaryLine
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", ConfigurationName="WSCreateProjectDiaryLine.WSJobJournalLine_Port")]
    public interface WSJobJournalLine_Port
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/page/wsjobjournalline:Read", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSCreateProjectDiaryLine.Read_Result> ReadAsync(WSCreateProjectDiaryLine.Read request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/page/wsjobjournalline:ReadByRecId", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSCreateProjectDiaryLine.ReadByRecId_Result> ReadByRecIdAsync(WSCreateProjectDiaryLine.ReadByRecId request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/page/wsjobjournalline:ReadMultiple", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSCreateProjectDiaryLine.ReadMultiple_Result> ReadMultipleAsync(WSCreateProjectDiaryLine.ReadMultiple request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/page/wsjobjournalline:IsUpdated", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSCreateProjectDiaryLine.IsUpdated_Result> IsUpdatedAsync(WSCreateProjectDiaryLine.IsUpdated request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/page/wsjobjournalline:GetRecIdFromKey", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSCreateProjectDiaryLine.GetRecIdFromKey_Result> GetRecIdFromKeyAsync(WSCreateProjectDiaryLine.GetRecIdFromKey request);
        
        // CODEGEN: Gerando contrato de mensagem porque a operação tem vários valores retornados.
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/page/wsjobjournalline:Create", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSCreateProjectDiaryLine.Create_Result> CreateAsync(WSCreateProjectDiaryLine.Create request);
        
        // CODEGEN: Gerando contrato de mensagem porque a operação tem vários valores retornados.
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/page/wsjobjournalline:CreateMultiple", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSCreateProjectDiaryLine.CreateMultiple_Result> CreateMultipleAsync(WSCreateProjectDiaryLine.CreateMultiple request);
        
        // CODEGEN: Gerando contrato de mensagem porque a operação tem vários valores retornados.
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/page/wsjobjournalline:Update", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSCreateProjectDiaryLine.Update_Result> UpdateAsync(WSCreateProjectDiaryLine.Update request);
        
        // CODEGEN: Gerando contrato de mensagem porque a operação tem vários valores retornados.
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/page/wsjobjournalline:UpdateMultiple", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSCreateProjectDiaryLine.UpdateMultiple_Result> UpdateMultipleAsync(WSCreateProjectDiaryLine.UpdateMultiple request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/page/wsjobjournalline:Delete", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSCreateProjectDiaryLine.Delete_Result> DeleteAsync(WSCreateProjectDiaryLine.Delete request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline")]
    public partial class WSJobJournalLine
    {
        
        private string keyField;
        
        private int line_NoField;
        
        private bool line_NoFieldSpecified;
        
        private string job_NoField;
        
        private System.DateTime document_DateField;
        
        private bool document_DateFieldSpecified;
        
        private Entry_Type entry_TypeField;
        
        private bool entry_TypeFieldSpecified;
        
        private string document_NoField;
        
        private Type typeField;
        
        private bool typeFieldSpecified;
        
        private string noField;
        
        private string description100Field;
        
        private decimal quantityField;
        
        private bool quantityFieldSpecified;
        
        private string unit_of_Measure_CodeField;
        
        private string location_CodeField;
        
        private string posting_GroupField;
        
        private string regionCode20Field;
        
        private string functionAreaCode20Field;
        
        private string responsabilityCenterCode20Field;
        
        private decimal unit_CostField;
        
        private bool unit_CostFieldSpecified;
        
        private decimal total_CostField;
        
        private bool total_CostFieldSpecified;
        
        private decimal unit_PriceField;
        
        private bool unit_PriceFieldSpecified;
        
        private decimal total_PriceField;
        
        private bool total_PriceFieldSpecified;
        
        private bool chargeableField;
        
        private bool chargeableFieldSpecified;
        
        private string external_Document_NoField;
        
        private string time_Sheet_NoField;
        
        private string portal_Transaction_NoField;
        
        private System.DateTime posting_DateField;
        
        private bool posting_DateFieldSpecified;
        
        private int oM_Line_NoField;
        
        private bool oM_Line_NoFieldSpecified;
        
        private string descriptionField;
        
        private string description_2Field;
        
        private decimal gDec1Field;
        
        private bool gDec1FieldSpecified;
        
        private decimal gDec2Field;
        
        private bool gDec2FieldSpecified;
        
        private decimal gDec3Field;
        
        private bool gDec3FieldSpecified;
        
        private decimal gDec4Field;
        
        private bool gDec4FieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Key
        {
            get
            {
                return this.keyField;
            }
            set
            {
                this.keyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public int Line_No
        {
            get
            {
                return this.line_NoField;
            }
            set
            {
                this.line_NoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Line_NoSpecified
        {
            get
            {
                return this.line_NoFieldSpecified;
            }
            set
            {
                this.line_NoFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Job_No
        {
            get
            {
                return this.job_NoField;
            }
            set
            {
                this.job_NoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date", Order=3)]
        public System.DateTime Document_Date
        {
            get
            {
                return this.document_DateField;
            }
            set
            {
                this.document_DateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Document_DateSpecified
        {
            get
            {
                return this.document_DateFieldSpecified;
            }
            set
            {
                this.document_DateFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public Entry_Type Entry_Type
        {
            get
            {
                return this.entry_TypeField;
            }
            set
            {
                this.entry_TypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Entry_TypeSpecified
        {
            get
            {
                return this.entry_TypeFieldSpecified;
            }
            set
            {
                this.entry_TypeFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string Document_No
        {
            get
            {
                return this.document_NoField;
            }
            set
            {
                this.document_NoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public Type Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TypeSpecified
        {
            get
            {
                return this.typeFieldSpecified;
            }
            set
            {
                this.typeFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public string No
        {
            get
            {
                return this.noField;
            }
            set
            {
                this.noField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string Description100
        {
            get
            {
                return this.description100Field;
            }
            set
            {
                this.description100Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public decimal Quantity
        {
            get
            {
                return this.quantityField;
            }
            set
            {
                this.quantityField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool QuantitySpecified
        {
            get
            {
                return this.quantityFieldSpecified;
            }
            set
            {
                this.quantityFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=10)]
        public string Unit_of_Measure_Code
        {
            get
            {
                return this.unit_of_Measure_CodeField;
            }
            set
            {
                this.unit_of_Measure_CodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=11)]
        public string Location_Code
        {
            get
            {
                return this.location_CodeField;
            }
            set
            {
                this.location_CodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=12)]
        public string Posting_Group
        {
            get
            {
                return this.posting_GroupField;
            }
            set
            {
                this.posting_GroupField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=13)]
        public string RegionCode20
        {
            get
            {
                return this.regionCode20Field;
            }
            set
            {
                this.regionCode20Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=14)]
        public string FunctionAreaCode20
        {
            get
            {
                return this.functionAreaCode20Field;
            }
            set
            {
                this.functionAreaCode20Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=15)]
        public string ResponsabilityCenterCode20
        {
            get
            {
                return this.responsabilityCenterCode20Field;
            }
            set
            {
                this.responsabilityCenterCode20Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=16)]
        public decimal Unit_Cost
        {
            get
            {
                return this.unit_CostField;
            }
            set
            {
                this.unit_CostField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Unit_CostSpecified
        {
            get
            {
                return this.unit_CostFieldSpecified;
            }
            set
            {
                this.unit_CostFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=17)]
        public decimal Total_Cost
        {
            get
            {
                return this.total_CostField;
            }
            set
            {
                this.total_CostField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Total_CostSpecified
        {
            get
            {
                return this.total_CostFieldSpecified;
            }
            set
            {
                this.total_CostFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=18)]
        public decimal Unit_Price
        {
            get
            {
                return this.unit_PriceField;
            }
            set
            {
                this.unit_PriceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Unit_PriceSpecified
        {
            get
            {
                return this.unit_PriceFieldSpecified;
            }
            set
            {
                this.unit_PriceFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=19)]
        public decimal Total_Price
        {
            get
            {
                return this.total_PriceField;
            }
            set
            {
                this.total_PriceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Total_PriceSpecified
        {
            get
            {
                return this.total_PriceFieldSpecified;
            }
            set
            {
                this.total_PriceFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=20)]
        public bool Chargeable
        {
            get
            {
                return this.chargeableField;
            }
            set
            {
                this.chargeableField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ChargeableSpecified
        {
            get
            {
                return this.chargeableFieldSpecified;
            }
            set
            {
                this.chargeableFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=21)]
        public string External_Document_No
        {
            get
            {
                return this.external_Document_NoField;
            }
            set
            {
                this.external_Document_NoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=22)]
        public string Time_Sheet_No
        {
            get
            {
                return this.time_Sheet_NoField;
            }
            set
            {
                this.time_Sheet_NoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=23)]
        public string Portal_Transaction_No
        {
            get
            {
                return this.portal_Transaction_NoField;
            }
            set
            {
                this.portal_Transaction_NoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date", Order=24)]
        public System.DateTime Posting_Date
        {
            get
            {
                return this.posting_DateField;
            }
            set
            {
                this.posting_DateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Posting_DateSpecified
        {
            get
            {
                return this.posting_DateFieldSpecified;
            }
            set
            {
                this.posting_DateFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=25)]
        public int OM_Line_No
        {
            get
            {
                return this.oM_Line_NoField;
            }
            set
            {
                this.oM_Line_NoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OM_Line_NoSpecified
        {
            get
            {
                return this.oM_Line_NoFieldSpecified;
            }
            set
            {
                this.oM_Line_NoFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=26)]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=27)]
        public string Description_2
        {
            get
            {
                return this.description_2Field;
            }
            set
            {
                this.description_2Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=28)]
        public decimal gDec1
        {
            get
            {
                return this.gDec1Field;
            }
            set
            {
                this.gDec1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool gDec1Specified
        {
            get
            {
                return this.gDec1FieldSpecified;
            }
            set
            {
                this.gDec1FieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=29)]
        public decimal gDec2
        {
            get
            {
                return this.gDec2Field;
            }
            set
            {
                this.gDec2Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool gDec2Specified
        {
            get
            {
                return this.gDec2FieldSpecified;
            }
            set
            {
                this.gDec2FieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=30)]
        public decimal gDec3
        {
            get
            {
                return this.gDec3Field;
            }
            set
            {
                this.gDec3Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool gDec3Specified
        {
            get
            {
                return this.gDec3FieldSpecified;
            }
            set
            {
                this.gDec3FieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=31)]
        public decimal gDec4
        {
            get
            {
                return this.gDec4Field;
            }
            set
            {
                this.gDec4Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool gDec4Specified
        {
            get
            {
                return this.gDec4FieldSpecified;
            }
            set
            {
                this.gDec4FieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline")]
    public enum Entry_Type
    {
        
        /// <remarks/>
        Usage,
        
        /// <remarks/>
        Sale,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline")]
    public enum Type
    {
        
        /// <remarks/>
        Resource,
        
        /// <remarks/>
        Item,
        
        /// <remarks/>
        G_L_Account,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline")]
    public partial class WSJobJournalLine_Filter
    {
        
        private WSJobJournalLine_Fields fieldField;
        
        private string criteriaField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public WSJobJournalLine_Fields Field
        {
            get
            {
                return this.fieldField;
            }
            set
            {
                this.fieldField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Criteria
        {
            get
            {
                return this.criteriaField;
            }
            set
            {
                this.criteriaField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline")]
    public enum WSJobJournalLine_Fields
    {
        
        /// <remarks/>
        Line_No,
        
        /// <remarks/>
        Job_No,
        
        /// <remarks/>
        Document_Date,
        
        /// <remarks/>
        Entry_Type,
        
        /// <remarks/>
        Document_No,
        
        /// <remarks/>
        Type,
        
        /// <remarks/>
        No,
        
        /// <remarks/>
        Description100,
        
        /// <remarks/>
        Quantity,
        
        /// <remarks/>
        Unit_of_Measure_Code,
        
        /// <remarks/>
        Location_Code,
        
        /// <remarks/>
        Posting_Group,
        
        /// <remarks/>
        RegionCode20,
        
        /// <remarks/>
        FunctionAreaCode20,
        
        /// <remarks/>
        ResponsabilityCenterCode20,
        
        /// <remarks/>
        Unit_Cost,
        
        /// <remarks/>
        Total_Cost,
        
        /// <remarks/>
        Unit_Price,
        
        /// <remarks/>
        Total_Price,
        
        /// <remarks/>
        Chargeable,
        
        /// <remarks/>
        External_Document_No,
        
        /// <remarks/>
        Time_Sheet_No,
        
        /// <remarks/>
        Portal_Transaction_No,
        
        /// <remarks/>
        Posting_Date,
        
        /// <remarks/>
        OM_Line_No,
        
        /// <remarks/>
        Description,
        
        /// <remarks/>
        Description_2,
        
        /// <remarks/>
        gDec1,
        
        /// <remarks/>
        gDec2,
        
        /// <remarks/>
        gDec3,
        
        /// <remarks/>
        gDec4,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Read", WrapperNamespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", IsWrapped=true)]
    public partial class Read
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", Order=0)]
        public int Line_No;
        
        public Read()
        {
        }
        
        public Read(int Line_No)
        {
            this.Line_No = Line_No;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Read_Result", WrapperNamespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", IsWrapped=true)]
    public partial class Read_Result
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", Order=0)]
        public WSCreateProjectDiaryLine.WSJobJournalLine WSJobJournalLine;
        
        public Read_Result()
        {
        }
        
        public Read_Result(WSCreateProjectDiaryLine.WSJobJournalLine WSJobJournalLine)
        {
            this.WSJobJournalLine = WSJobJournalLine;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ReadByRecId", WrapperNamespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", IsWrapped=true)]
    public partial class ReadByRecId
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", Order=0)]
        public string recId;
        
        public ReadByRecId()
        {
        }
        
        public ReadByRecId(string recId)
        {
            this.recId = recId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ReadByRecId_Result", WrapperNamespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", IsWrapped=true)]
    public partial class ReadByRecId_Result
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", Order=0)]
        public WSCreateProjectDiaryLine.WSJobJournalLine WSJobJournalLine;
        
        public ReadByRecId_Result()
        {
        }
        
        public ReadByRecId_Result(WSCreateProjectDiaryLine.WSJobJournalLine WSJobJournalLine)
        {
            this.WSJobJournalLine = WSJobJournalLine;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ReadMultiple", WrapperNamespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", IsWrapped=true)]
    public partial class ReadMultiple
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("filter")]
        public WSCreateProjectDiaryLine.WSJobJournalLine_Filter[] filter;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", Order=1)]
        public string bookmarkKey;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", Order=2)]
        public int setSize;
        
        public ReadMultiple()
        {
        }
        
        public ReadMultiple(WSCreateProjectDiaryLine.WSJobJournalLine_Filter[] filter, string bookmarkKey, int setSize)
        {
            this.filter = filter;
            this.bookmarkKey = bookmarkKey;
            this.setSize = setSize;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ReadMultiple_Result", WrapperNamespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", IsWrapped=true)]
    public partial class ReadMultiple_Result
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ReadMultiple_Result", Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public WSCreateProjectDiaryLine.WSJobJournalLine[] ReadMultiple_Result1;
        
        public ReadMultiple_Result()
        {
        }
        
        public ReadMultiple_Result(WSCreateProjectDiaryLine.WSJobJournalLine[] ReadMultiple_Result1)
        {
            this.ReadMultiple_Result1 = ReadMultiple_Result1;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="IsUpdated", WrapperNamespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", IsWrapped=true)]
    public partial class IsUpdated
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", Order=0)]
        public string Key;
        
        public IsUpdated()
        {
        }
        
        public IsUpdated(string Key)
        {
            this.Key = Key;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="IsUpdated_Result", WrapperNamespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", IsWrapped=true)]
    public partial class IsUpdated_Result
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="IsUpdated_Result", Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", Order=0)]
        public bool IsUpdated_Result1;
        
        public IsUpdated_Result()
        {
        }
        
        public IsUpdated_Result(bool IsUpdated_Result1)
        {
            this.IsUpdated_Result1 = IsUpdated_Result1;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetRecIdFromKey", WrapperNamespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", IsWrapped=true)]
    public partial class GetRecIdFromKey
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", Order=0)]
        public string Key;
        
        public GetRecIdFromKey()
        {
        }
        
        public GetRecIdFromKey(string Key)
        {
            this.Key = Key;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetRecIdFromKey_Result", WrapperNamespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", IsWrapped=true)]
    public partial class GetRecIdFromKey_Result
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetRecIdFromKey_Result", Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", Order=0)]
        public string GetRecIdFromKey_Result1;
        
        public GetRecIdFromKey_Result()
        {
        }
        
        public GetRecIdFromKey_Result(string GetRecIdFromKey_Result1)
        {
            this.GetRecIdFromKey_Result1 = GetRecIdFromKey_Result1;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Create", WrapperNamespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", IsWrapped=true)]
    public partial class Create
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", Order=0)]
        public WSCreateProjectDiaryLine.WSJobJournalLine WSJobJournalLine;
        
        public Create()
        {
        }
        
        public Create(WSCreateProjectDiaryLine.WSJobJournalLine WSJobJournalLine)
        {
            this.WSJobJournalLine = WSJobJournalLine;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Create_Result", WrapperNamespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", IsWrapped=true)]
    public partial class Create_Result
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", Order=0)]
        public WSCreateProjectDiaryLine.WSJobJournalLine WSJobJournalLine;
        
        public Create_Result()
        {
        }
        
        public Create_Result(WSCreateProjectDiaryLine.WSJobJournalLine WSJobJournalLine)
        {
            this.WSJobJournalLine = WSJobJournalLine;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="CreateMultiple", WrapperNamespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", IsWrapped=true)]
    public partial class CreateMultiple
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public WSCreateProjectDiaryLine.WSJobJournalLine[] WSJobJournalLine_List;
        
        public CreateMultiple()
        {
        }
        
        public CreateMultiple(WSCreateProjectDiaryLine.WSJobJournalLine[] WSJobJournalLine_List)
        {
            this.WSJobJournalLine_List = WSJobJournalLine_List;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="CreateMultiple_Result", WrapperNamespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", IsWrapped=true)]
    public partial class CreateMultiple_Result
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public WSCreateProjectDiaryLine.WSJobJournalLine[] WSJobJournalLine_List;
        
        public CreateMultiple_Result()
        {
        }
        
        public CreateMultiple_Result(WSCreateProjectDiaryLine.WSJobJournalLine[] WSJobJournalLine_List)
        {
            this.WSJobJournalLine_List = WSJobJournalLine_List;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Update", WrapperNamespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", IsWrapped=true)]
    public partial class Update
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", Order=0)]
        public WSCreateProjectDiaryLine.WSJobJournalLine WSJobJournalLine;
        
        public Update()
        {
        }
        
        public Update(WSCreateProjectDiaryLine.WSJobJournalLine WSJobJournalLine)
        {
            this.WSJobJournalLine = WSJobJournalLine;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Update_Result", WrapperNamespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", IsWrapped=true)]
    public partial class Update_Result
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", Order=0)]
        public WSCreateProjectDiaryLine.WSJobJournalLine WSJobJournalLine;
        
        public Update_Result()
        {
        }
        
        public Update_Result(WSCreateProjectDiaryLine.WSJobJournalLine WSJobJournalLine)
        {
            this.WSJobJournalLine = WSJobJournalLine;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UpdateMultiple", WrapperNamespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", IsWrapped=true)]
    public partial class UpdateMultiple
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public WSCreateProjectDiaryLine.WSJobJournalLine[] WSJobJournalLine_List;
        
        public UpdateMultiple()
        {
        }
        
        public UpdateMultiple(WSCreateProjectDiaryLine.WSJobJournalLine[] WSJobJournalLine_List)
        {
            this.WSJobJournalLine_List = WSJobJournalLine_List;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UpdateMultiple_Result", WrapperNamespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", IsWrapped=true)]
    public partial class UpdateMultiple_Result
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public WSCreateProjectDiaryLine.WSJobJournalLine[] WSJobJournalLine_List;
        
        public UpdateMultiple_Result()
        {
        }
        
        public UpdateMultiple_Result(WSCreateProjectDiaryLine.WSJobJournalLine[] WSJobJournalLine_List)
        {
            this.WSJobJournalLine_List = WSJobJournalLine_List;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Delete", WrapperNamespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", IsWrapped=true)]
    public partial class Delete
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", Order=0)]
        public string Key;
        
        public Delete()
        {
        }
        
        public Delete(string Key)
        {
            this.Key = Key;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Delete_Result", WrapperNamespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", IsWrapped=true)]
    public partial class Delete_Result
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Delete_Result", Namespace="urn:microsoft-dynamics-schemas/page/wsjobjournalline", Order=0)]
        public bool Delete_Result1;
        
        public Delete_Result()
        {
        }
        
        public Delete_Result(bool Delete_Result1)
        {
            this.Delete_Result1 = Delete_Result1;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public interface WSJobJournalLine_PortChannel : WSCreateProjectDiaryLine.WSJobJournalLine_Port, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public partial class WSJobJournalLine_PortClient : System.ServiceModel.ClientBase<WSCreateProjectDiaryLine.WSJobJournalLine_Port>, WSCreateProjectDiaryLine.WSJobJournalLine_Port
    {
        
    /// <summary>
    /// Implemente este método parcial para configurar o ponto de extremidade de serviço.
    /// </summary>
    /// <param name="serviceEndpoint">O ponto de extremidade a ser configurado</param>
    /// <param name="clientCredentials">As credenciais do cliente</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public WSJobJournalLine_PortClient() : 
                base(WSJobJournalLine_PortClient.GetDefaultBinding(), WSJobJournalLine_PortClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.WSJobJournalLine_Port.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public WSJobJournalLine_PortClient(EndpointConfiguration endpointConfiguration) : 
                base(WSJobJournalLine_PortClient.GetBindingForEndpoint(endpointConfiguration), WSJobJournalLine_PortClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public WSJobJournalLine_PortClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(WSJobJournalLine_PortClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public WSJobJournalLine_PortClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(WSJobJournalLine_PortClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public WSJobJournalLine_PortClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WSCreateProjectDiaryLine.Read_Result> WSCreateProjectDiaryLine.WSJobJournalLine_Port.ReadAsync(WSCreateProjectDiaryLine.Read request)
        {
            return base.Channel.ReadAsync(request);
        }
        
        public System.Threading.Tasks.Task<WSCreateProjectDiaryLine.Read_Result> ReadAsync(int Line_No)
        {
            WSCreateProjectDiaryLine.Read inValue = new WSCreateProjectDiaryLine.Read();
            inValue.Line_No = Line_No;
            return ((WSCreateProjectDiaryLine.WSJobJournalLine_Port)(this)).ReadAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WSCreateProjectDiaryLine.ReadByRecId_Result> WSCreateProjectDiaryLine.WSJobJournalLine_Port.ReadByRecIdAsync(WSCreateProjectDiaryLine.ReadByRecId request)
        {
            return base.Channel.ReadByRecIdAsync(request);
        }
        
        public System.Threading.Tasks.Task<WSCreateProjectDiaryLine.ReadByRecId_Result> ReadByRecIdAsync(string recId)
        {
            WSCreateProjectDiaryLine.ReadByRecId inValue = new WSCreateProjectDiaryLine.ReadByRecId();
            inValue.recId = recId;
            return ((WSCreateProjectDiaryLine.WSJobJournalLine_Port)(this)).ReadByRecIdAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WSCreateProjectDiaryLine.ReadMultiple_Result> WSCreateProjectDiaryLine.WSJobJournalLine_Port.ReadMultipleAsync(WSCreateProjectDiaryLine.ReadMultiple request)
        {
            return base.Channel.ReadMultipleAsync(request);
        }
        
        public System.Threading.Tasks.Task<WSCreateProjectDiaryLine.ReadMultiple_Result> ReadMultipleAsync(WSCreateProjectDiaryLine.WSJobJournalLine_Filter[] filter, string bookmarkKey, int setSize)
        {
            WSCreateProjectDiaryLine.ReadMultiple inValue = new WSCreateProjectDiaryLine.ReadMultiple();
            inValue.filter = filter;
            inValue.bookmarkKey = bookmarkKey;
            inValue.setSize = setSize;
            return ((WSCreateProjectDiaryLine.WSJobJournalLine_Port)(this)).ReadMultipleAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WSCreateProjectDiaryLine.IsUpdated_Result> WSCreateProjectDiaryLine.WSJobJournalLine_Port.IsUpdatedAsync(WSCreateProjectDiaryLine.IsUpdated request)
        {
            return base.Channel.IsUpdatedAsync(request);
        }
        
        public System.Threading.Tasks.Task<WSCreateProjectDiaryLine.IsUpdated_Result> IsUpdatedAsync(string Key)
        {
            WSCreateProjectDiaryLine.IsUpdated inValue = new WSCreateProjectDiaryLine.IsUpdated();
            inValue.Key = Key;
            return ((WSCreateProjectDiaryLine.WSJobJournalLine_Port)(this)).IsUpdatedAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WSCreateProjectDiaryLine.GetRecIdFromKey_Result> WSCreateProjectDiaryLine.WSJobJournalLine_Port.GetRecIdFromKeyAsync(WSCreateProjectDiaryLine.GetRecIdFromKey request)
        {
            return base.Channel.GetRecIdFromKeyAsync(request);
        }
        
        public System.Threading.Tasks.Task<WSCreateProjectDiaryLine.GetRecIdFromKey_Result> GetRecIdFromKeyAsync(string Key)
        {
            WSCreateProjectDiaryLine.GetRecIdFromKey inValue = new WSCreateProjectDiaryLine.GetRecIdFromKey();
            inValue.Key = Key;
            return ((WSCreateProjectDiaryLine.WSJobJournalLine_Port)(this)).GetRecIdFromKeyAsync(inValue);
        }
        
        public System.Threading.Tasks.Task<WSCreateProjectDiaryLine.Create_Result> CreateAsync(WSCreateProjectDiaryLine.Create request)
        {
            return base.Channel.CreateAsync(request);
        }
        
        public System.Threading.Tasks.Task<WSCreateProjectDiaryLine.CreateMultiple_Result> CreateMultipleAsync(WSCreateProjectDiaryLine.CreateMultiple request)
        {
            return base.Channel.CreateMultipleAsync(request);
        }
        
        public System.Threading.Tasks.Task<WSCreateProjectDiaryLine.Update_Result> UpdateAsync(WSCreateProjectDiaryLine.Update request)
        {
            return base.Channel.UpdateAsync(request);
        }
        
        public System.Threading.Tasks.Task<WSCreateProjectDiaryLine.UpdateMultiple_Result> UpdateMultipleAsync(WSCreateProjectDiaryLine.UpdateMultiple request)
        {
            return base.Channel.UpdateMultipleAsync(request);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WSCreateProjectDiaryLine.Delete_Result> WSCreateProjectDiaryLine.WSJobJournalLine_Port.DeleteAsync(WSCreateProjectDiaryLine.Delete request)
        {
            return base.Channel.DeleteAsync(request);
        }
        
        public System.Threading.Tasks.Task<WSCreateProjectDiaryLine.Delete_Result> DeleteAsync(string Key)
        {
            WSCreateProjectDiaryLine.Delete inValue = new WSCreateProjectDiaryLine.Delete();
            inValue.Key = Key;
            return ((WSCreateProjectDiaryLine.WSJobJournalLine_Port)(this)).DeleteAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.WSJobJournalLine_Port))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Não foi possível encontrar o ponto de extremidade com o nome \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.WSJobJournalLine_Port))
            {
                return new System.ServiceModel.EndpointAddress("http://such-navdev.such.local:8047/DynamicsNAV100_QUAL/WS/SUCH - QUALIDADE/Page/W" +
                        "SJobJournalLine?wsdl");
            }
            throw new System.InvalidOperationException(string.Format("Não foi possível encontrar o ponto de extremidade com o nome \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return WSJobJournalLine_PortClient.GetBindingForEndpoint(EndpointConfiguration.WSJobJournalLine_Port);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return WSJobJournalLine_PortClient.GetEndpointAddress(EndpointConfiguration.WSJobJournalLine_Port);
        }
        
        public enum EndpointConfiguration
        {
            
            WSJobJournalLine_Port,
        }
    }
}
