﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Main.TimboxWSCancelacion {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:WashOut", ConfigurationName="TimboxWSCancelacion.cancelacion_port")]
    public interface cancelacion_port {
        
        [System.ServiceModel.OperationContractAttribute(Action="cancelar_cfdi", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="cancelar_cfdi_result")]
        Main.TimboxWSCancelacion.cancelar_cfdi_result cancelar_cfdi(string username, string password, string rfc_emisor, Main.TimboxWSCancelacion.folios folios, string cert_pem, string llave_pem);
        
        [System.ServiceModel.OperationContractAttribute(Action="cancelar_cfdi", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="cancelar_cfdi_result")]
        System.Threading.Tasks.Task<Main.TimboxWSCancelacion.cancelar_cfdi_result> cancelar_cfdiAsync(string username, string password, string rfc_emisor, Main.TimboxWSCancelacion.folios folios, string cert_pem, string llave_pem);
        
        [System.ServiceModel.OperationContractAttribute(Action="consultar_estatus", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="consultar_estatus_result")]
        Main.TimboxWSCancelacion.consultar_estatus_result consultar_estatus(string username, string password, string uuid, string rfc_emisor, string rfc_receptor, string total);
        
        [System.ServiceModel.OperationContractAttribute(Action="consultar_estatus", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="consultar_estatus_result")]
        System.Threading.Tasks.Task<Main.TimboxWSCancelacion.consultar_estatus_result> consultar_estatusAsync(string username, string password, string uuid, string rfc_emisor, string rfc_receptor, string total);
        
        [System.ServiceModel.OperationContractAttribute(Action="consultar_peticiones_pendientes", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="consultar_peticiones_pendientes_result")]
        Main.TimboxWSCancelacion.consultar_peticiones_pendientes_result consultar_peticiones_pendientes(string username, string password, string rfc_receptor, string cert_pem, string llave_pem);
        
        [System.ServiceModel.OperationContractAttribute(Action="consultar_peticiones_pendientes", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="consultar_peticiones_pendientes_result")]
        System.Threading.Tasks.Task<Main.TimboxWSCancelacion.consultar_peticiones_pendientes_result> consultar_peticiones_pendientesAsync(string username, string password, string rfc_receptor, string cert_pem, string llave_pem);
        
        [System.ServiceModel.OperationContractAttribute(Action="procesar_respuesta", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="procesar_respuesta_result")]
        Main.TimboxWSCancelacion.procesar_respuesta_result procesar_respuesta(string username, string password, string rfc_receptor, Main.TimboxWSCancelacion.respuestas respuestas, string cert_pem, string llave_pem);
        
        [System.ServiceModel.OperationContractAttribute(Action="procesar_respuesta", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="procesar_respuesta_result")]
        System.Threading.Tasks.Task<Main.TimboxWSCancelacion.procesar_respuesta_result> procesar_respuestaAsync(string username, string password, string rfc_receptor, Main.TimboxWSCancelacion.respuestas respuestas, string cert_pem, string llave_pem);
        
        [System.ServiceModel.OperationContractAttribute(Action="consultar_documento_relacionado", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="consultar_documento_relacionado_result")]
        Main.TimboxWSCancelacion.consultar_documento_relacionado_result consultar_documento_relacionado(string username, string password, string uuid, string rfc_receptor, string cert_pem, string llave_pem);
        
        [System.ServiceModel.OperationContractAttribute(Action="consultar_documento_relacionado", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="consultar_documento_relacionado_result")]
        System.Threading.Tasks.Task<Main.TimboxWSCancelacion.consultar_documento_relacionado_result> consultar_documento_relacionadoAsync(string username, string password, string uuid, string rfc_receptor, string cert_pem, string llave_pem);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3163.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class folios : object, System.ComponentModel.INotifyPropertyChanged {
        
        private folio[] folioField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public folio[] folio {
            get {
                return this.folioField;
            }
            set {
                this.folioField = value;
                this.RaisePropertyChanged("folio");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3163.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class folio : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string uuidField;
        
        private string rfc_receptorField;
        
        private string totalField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string uuid {
            get {
                return this.uuidField;
            }
            set {
                this.uuidField = value;
                this.RaisePropertyChanged("uuid");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string rfc_receptor {
            get {
                return this.rfc_receptorField;
            }
            set {
                this.rfc_receptorField = value;
                this.RaisePropertyChanged("rfc_receptor");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string total {
            get {
                return this.totalField;
            }
            set {
                this.totalField = value;
                this.RaisePropertyChanged("total");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3163.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class consultar_documento_relacionado_result : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string resultadoField;
        
        private string relacionados_padresField;
        
        private string relacionados_hijosField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string resultado {
            get {
                return this.resultadoField;
            }
            set {
                this.resultadoField = value;
                this.RaisePropertyChanged("resultado");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string relacionados_padres {
            get {
                return this.relacionados_padresField;
            }
            set {
                this.relacionados_padresField = value;
                this.RaisePropertyChanged("relacionados_padres");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string relacionados_hijos {
            get {
                return this.relacionados_hijosField;
            }
            set {
                this.relacionados_hijosField = value;
                this.RaisePropertyChanged("relacionados_hijos");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3163.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class procesar_respuesta_result : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string foliosField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string folios {
            get {
                return this.foliosField;
            }
            set {
                this.foliosField = value;
                this.RaisePropertyChanged("folios");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3163.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class folios_respuestas : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string uuidField;
        
        private string rfc_emisorField;
        
        private string totalField;
        
        private string respuestaField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string uuid {
            get {
                return this.uuidField;
            }
            set {
                this.uuidField = value;
                this.RaisePropertyChanged("uuid");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string rfc_emisor {
            get {
                return this.rfc_emisorField;
            }
            set {
                this.rfc_emisorField = value;
                this.RaisePropertyChanged("rfc_emisor");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string total {
            get {
                return this.totalField;
            }
            set {
                this.totalField = value;
                this.RaisePropertyChanged("total");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string respuesta {
            get {
                return this.respuestaField;
            }
            set {
                this.respuestaField = value;
                this.RaisePropertyChanged("respuesta");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3163.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class respuestas : object, System.ComponentModel.INotifyPropertyChanged {
        
        private folios_respuestas[] folios_respuestasField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public folios_respuestas[] folios_respuestas {
            get {
                return this.folios_respuestasField;
            }
            set {
                this.folios_respuestasField = value;
                this.RaisePropertyChanged("folios_respuestas");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3163.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class uuids : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string[] uuidField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string[] uuid {
            get {
                return this.uuidField;
            }
            set {
                this.uuidField = value;
                this.RaisePropertyChanged("uuid");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3163.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class consultar_peticiones_pendientes_result : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string codestatusField;
        
        private uuids uuidsField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string codestatus {
            get {
                return this.codestatusField;
            }
            set {
                this.codestatusField = value;
                this.RaisePropertyChanged("codestatus");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public uuids uuids {
            get {
                return this.uuidsField;
            }
            set {
                this.uuidsField = value;
                this.RaisePropertyChanged("uuids");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3163.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class consultar_estatus_result : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string codigo_estatusField;
        
        private string es_cancelableField;
        
        private string estadoField;
        
        private string estatus_cancelacionField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string codigo_estatus {
            get {
                return this.codigo_estatusField;
            }
            set {
                this.codigo_estatusField = value;
                this.RaisePropertyChanged("codigo_estatus");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string es_cancelable {
            get {
                return this.es_cancelableField;
            }
            set {
                this.es_cancelableField = value;
                this.RaisePropertyChanged("es_cancelable");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string estado {
            get {
                return this.estadoField;
            }
            set {
                this.estadoField = value;
                this.RaisePropertyChanged("estado");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string estatus_cancelacion {
            get {
                return this.estatus_cancelacionField;
            }
            set {
                this.estatus_cancelacionField = value;
                this.RaisePropertyChanged("estatus_cancelacion");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3163.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class cancelar_cfdi_result : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string folios_cancelacionField;
        
        private string acuse_cancelacionField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string folios_cancelacion {
            get {
                return this.folios_cancelacionField;
            }
            set {
                this.folios_cancelacionField = value;
                this.RaisePropertyChanged("folios_cancelacion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string acuse_cancelacion {
            get {
                return this.acuse_cancelacionField;
            }
            set {
                this.acuse_cancelacionField = value;
                this.RaisePropertyChanged("acuse_cancelacion");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface cancelacion_portChannel : Main.TimboxWSCancelacion.cancelacion_port, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class cancelacion_portClient : System.ServiceModel.ClientBase<Main.TimboxWSCancelacion.cancelacion_port>, Main.TimboxWSCancelacion.cancelacion_port {
        
        public cancelacion_portClient() {
        }
        
        public cancelacion_portClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public cancelacion_portClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public cancelacion_portClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public cancelacion_portClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Main.TimboxWSCancelacion.cancelar_cfdi_result cancelar_cfdi(string username, string password, string rfc_emisor, Main.TimboxWSCancelacion.folios folios, string cert_pem, string llave_pem) {
            return base.Channel.cancelar_cfdi(username, password, rfc_emisor, folios, cert_pem, llave_pem);
        }
        
        public System.Threading.Tasks.Task<Main.TimboxWSCancelacion.cancelar_cfdi_result> cancelar_cfdiAsync(string username, string password, string rfc_emisor, Main.TimboxWSCancelacion.folios folios, string cert_pem, string llave_pem) {
            return base.Channel.cancelar_cfdiAsync(username, password, rfc_emisor, folios, cert_pem, llave_pem);
        }
        
        public Main.TimboxWSCancelacion.consultar_estatus_result consultar_estatus(string username, string password, string uuid, string rfc_emisor, string rfc_receptor, string total) {
            return base.Channel.consultar_estatus(username, password, uuid, rfc_emisor, rfc_receptor, total);
        }
        
        public System.Threading.Tasks.Task<Main.TimboxWSCancelacion.consultar_estatus_result> consultar_estatusAsync(string username, string password, string uuid, string rfc_emisor, string rfc_receptor, string total) {
            return base.Channel.consultar_estatusAsync(username, password, uuid, rfc_emisor, rfc_receptor, total);
        }
        
        public Main.TimboxWSCancelacion.consultar_peticiones_pendientes_result consultar_peticiones_pendientes(string username, string password, string rfc_receptor, string cert_pem, string llave_pem) {
            return base.Channel.consultar_peticiones_pendientes(username, password, rfc_receptor, cert_pem, llave_pem);
        }
        
        public System.Threading.Tasks.Task<Main.TimboxWSCancelacion.consultar_peticiones_pendientes_result> consultar_peticiones_pendientesAsync(string username, string password, string rfc_receptor, string cert_pem, string llave_pem) {
            return base.Channel.consultar_peticiones_pendientesAsync(username, password, rfc_receptor, cert_pem, llave_pem);
        }
        
        public Main.TimboxWSCancelacion.procesar_respuesta_result procesar_respuesta(string username, string password, string rfc_receptor, Main.TimboxWSCancelacion.respuestas respuestas, string cert_pem, string llave_pem) {
            return base.Channel.procesar_respuesta(username, password, rfc_receptor, respuestas, cert_pem, llave_pem);
        }
        
        public System.Threading.Tasks.Task<Main.TimboxWSCancelacion.procesar_respuesta_result> procesar_respuestaAsync(string username, string password, string rfc_receptor, Main.TimboxWSCancelacion.respuestas respuestas, string cert_pem, string llave_pem) {
            return base.Channel.procesar_respuestaAsync(username, password, rfc_receptor, respuestas, cert_pem, llave_pem);
        }
        
        public Main.TimboxWSCancelacion.consultar_documento_relacionado_result consultar_documento_relacionado(string username, string password, string uuid, string rfc_receptor, string cert_pem, string llave_pem) {
            return base.Channel.consultar_documento_relacionado(username, password, uuid, rfc_receptor, cert_pem, llave_pem);
        }
        
        public System.Threading.Tasks.Task<Main.TimboxWSCancelacion.consultar_documento_relacionado_result> consultar_documento_relacionadoAsync(string username, string password, string uuid, string rfc_receptor, string cert_pem, string llave_pem) {
            return base.Channel.consultar_documento_relacionadoAsync(username, password, uuid, rfc_receptor, cert_pem, llave_pem);
        }
    }
}
