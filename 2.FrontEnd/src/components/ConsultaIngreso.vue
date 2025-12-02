<template>
    <v-layout align-start>
        <v-flex>
            <v-toolbar flat color="white">
                <v-btn @click="crearPDFVentas()"><v-icon>print</v-icon></v-btn>

                <v-btn >
                    <export-excel
                    class   = "btn btn-default"
                    color="primary" 
                    :data   = "ingresos"
                    :fields = "json_fields"
                    worksheet = "Listado de Ingresos"
                    name    = "Listado de Ingresos.xls">
                    Exportar a Excel
                    </export-excel>
                </v-btn>


                <v-toolbar-title class="primary--text">Consulta Ingreso</v-toolbar-title>
                    <v-divider
                    class="mx-2"
                    inset
                    vertical
                    ></v-divider>
                    <v-spacer></v-spacer>

                    <!--Cajas de Busqueda -->
                    Desde:&nbsp;
                    <v-text-field type="date" v-if="verNuevo==0" 
                    class="text-xs-center" v-model="fechaInicio"></v-text-field>
                    Hasta:&nbsp;


                    <v-text-field type="date" v-if="verNuevo==0" 
                    class="text-xs-center" v-model="fechaFin"></v-text-field>
                    <v-btn v-if="verNuevo==0" @click="listar()" color="primary" dark class="mb-2">Buscar</v-btn>

                
 
                                                       
                </v-toolbar>
                
             <!--Tabla Ingresos -->

           <v-data-table
                :headers="headers"
                :items="ingresos" 
                class="elevation-1"          
                 v-if="verNuevo==0"
            >

                
                <template v-slot:item="props">
                     <tr>              
                        <td class="justify-center layout px-0">                          
                        <v-icon
                        small
                        class="mr-2"
                        @click="verDetalles(props.item)"
                        >
                        tab
                        </v-icon>
                        <!--Icono Mostrar Comprobante -->
                       

                                            
                        </td> 
                            
                        <td>{{ props.item.usuario }}</td>
                        <td >
                            <strong>
                                <div >
                                    <span class="black--text">
                                        {{ props.item.proveedor}}
                                    </span>
                                </div>
                            </strong>                       
                        </td>
                       
                        <td >
                            <strong>
                                <div >
                                    <span class="red--text">
                                        Q.{{ props.item.total.toFixed(2) }}
                                    </span>
                                </div>
                            </strong>                       
                        </td>
                        <td>{{ props.item.fechaHora }}</td>
                      
                        <td>{{ props.item.tipoComprobante }}</td>
                        <td>{{ props.item.serieComprobante }}</td>
                        
                        <td >
                            <strong>
                                <div >
                                    <span class="black--text">
                                        {{ props.item.numeroComprobante }}
                                    </span>
                                </div>
                            </strong>                       
                        </td>
                      
                        
                        <td>{{ props.item.descripcionCaja }}</td>
                        

                    

                         


                        <td >
                            <div v-if="props.item.estado=='Aceptado'">
                                <strong>
                                    <div>
                                        <span class="blue--text">Aceptado</span>
                                    </div>
                                </strong>
                           
                            </div>
                            <div v-else>
                                <strong>
                                    <div>
                                        <span class="red--text">{{props.item.estado}}</span>
                                    </div>
                                </strong>
                            
                            </div>
                        </td>
                    </tr>                  
                </template>
             
                    <template v-slot:no-data>

                        <v-btn color="primary" @click="listar">Resetear</v-btn>
                     </template>

           </v-data-table>


            
                 <!--Formulario -->
            <!-- NUEVO   --> 
            <v-container grid-list-sm fluid class="pa-4 white" v-if="verNuevo">
                <v-layout row wrap>

                    <!--tipo Comprobante -->
                    <v-flex xs12 sm4 md4 lg4 xl4>
                        <v-select
                        v-show=comprobantetabla
                        item-text= text
                        item-value = value
                        v-model="idcomprobante"
                        :items="listadocomprobante"
                        label="Comprobante">
                        </v-select>
                    </v-flex>

                    <!--serie -->
                    <v-flex xs12 sm4 md4 lg4 xl4 >
                        <v-select 
                        v-show=comprobantetabla
                        readonly                  
                        item-text= serie
                        item-value= value                       
                        v-model="idcomprobante" 
                        :items="listadocomprobante" 
                        label="Serie Comprobante">                        
                        </v-select>
                    </v-flex>
                
                    <!--correlativo -->
                    <v-flex xs12 sm4 md4 lg4 xl4 >
                        <v-select 
                        v-show=comprobantetabla
                        readonly                  
                        item-text= correlativo
                        item-value= value                       
                        v-model="idcomprobante" 
                        :items="listadocomprobante" 
                        label="Correlativo Comprobante">                        
                        </v-select>
                    </v-flex>

                    <!--Comprobante Ver-->
                    <v-flex xs12 sm4 md4 lg4 xl4>
                        <v-text-field
                        readonly 
                        v-show=comprobanteingreso
                        v-model="idcomprobante"
                        label="Comprobante">
                        </v-text-field>
                        </v-flex>

                        <!--Serie Ver -->
                    <v-flex xs12 sm4 md4 lg4 xl4>
                        <v-text-field
                        readonly 
                        v-show=comprobanteingreso
                        v-model="seriecomprobante"
                        label="Serie">
                        </v-text-field>
                    </v-flex>

                        <!--Correlativo Ver -->
                    <v-flex xs12 sm4 md4 lg4 xl4>
                        <v-text-field
                        readonly 
                        v-show=comprobanteingreso
                        v-model="correlativocomprobante"
                        label="Correlativo">
                        </v-text-field>
                    </v-flex>
                    
                        
                            
                    <!--Proveedor -->
                    <v-flex xs12 sm8 md8 lg8 xl8>
                        <v-select
                        :readonly="comprobanteingreso" 
                        v-model="idproveedor"
                        
                        :items="proveedores"
                         label="Proveedor">
                        </v-select>
                    </v-flex>

                   <!--Buscar Articulo -->
                    <v-flex xs12 sm8 md8 lg8 xl8 >
                        <v-text-field                         
                        @keyup.enter="buscarCodigo()"
                        v-model="codigo"
                        v-show=comprobantetabla
                        label="Código del Articulo">
                        </v-text-field>
                    </v-flex>

                    <v-flex xs12 sm2 md2 lg2 xl2>
                        <v-btn 
                           v-show=comprobantetabla
                           @click="mostrarArticulos()"
                           small fab dark color="teal">
                            <v-icon dark>list</v-icon>
                        </v-btn>
                    </v-flex>

                    <v-flex xs12 sm2 md2 lg2 xl2 v-if="errorArticulo">
                        <div class="red--text" v-text="errorArticulo">
                        </div>
                    </v-flex>
                   
                    <!--Listado Detalle -->
                    <v-flex xs12 sm12 md12 lg12 xl12>
                        <v-data-table
                            :headers="cabeceraDetalles"
                            :items="detalles"
                            hide-default-footer
                            class="elevation-1" > 
                                    
                            <template v-slot:item="props">
                                <tr>                                            
                                    <td class="justify-center layout px-0">
                                        
                                    <v-icon 
                                    v-show=comprobantetabla
                                    small class="mr-2"  
                                    @click="eliminarDetalle(detalles,props.item)">
                                    deleted
                                    </v-icon>
                                                                                                       
                                    </td>
                                    <td >
                                         <strong>
                                            <div >
                                                <span class="blue--text">
                                                    {{ props.item.codigo}}
                                                </span>
                                            </div>
                                        </strong>                       
                                    </td>

                                    <td >
                                         <strong>
                                            <div >
                                                <span class="black--text">
                                                    {{ props.item.articulo}}
                                                </span>
                                            </div>
                                        </strong>                       
                                    </td>                               
                                    <td><v-text-field type="number" v-model.number="props.item.cantidad"></v-text-field></td>                         
                                    <td >
                                         <strong>
                                            <div >
                                                <span class="red--text">
                                                    Q.{{ props.item.precio.toFixed(2) }}
                                                </span>
                                            </div>
                                        </strong>                       
                                    </td>
                                  
                                    

                                    
                                    <td>
                                        <strong>
                                            <div >
                                                <span class="black--text">
                                                    Q. {{ props.item.cantidad * props.item.precio}}
                                                </span>
                                            </div>
                                        </strong>  </td>
                                            
                                </tr>                  
                            </template>
                        
                            <template v-slot:no-data>
                               <h3>No hay artículos agregados al detalle.</h3>>
                            </template>

                        </v-data-table>

                        <v-flex class="text-xs-right">
                            <strong>Total Parcial: </strong>Q. {{totalParcial=(total-totalImpuesto).toFixed(2)}}
                        </v-flex>
                        <v-flex class="text-xs-right">
                            <strong>Total del Impuesto: </strong>Q. 
                        </v-flex>
                        <v-flex class="text-xs-right" text-h6 >
                            <strong>Total  Neto: </strong>
                            <strong>
                                        
                                <span class="red--text" >
                                    Q. {{total=(calcularTotal).toFixed(2)}}
                                </span>
                                          
                             </strong> 
                            
                        </v-flex>



                    </v-flex>

                    <v-flex xs12 sm12 md12 v-show="valida">
                        <div class="red--text" v-for="v in validaMensaje" :key="v" v-text="v">
                        </div>
                    </v-flex>

                   

                    <v-flex xs12 sm12 md12 lg12 xl12>
                        <v-btn @click="ocultarNuevo()"  color="blue darken-1" text>Cancelar</v-btn>
                        <v-btn v-if="verDet==0" @click="guardar()" color="success">Guardar</v-btn>
                    </v-flex>


		        </v-layout>
            </v-container>
            
        </v-flex>
    </v-layout>
</template>



<!-- Logica --> 

<script>

    import axios from 'axios'
    //JSPDF
    import jsPDF from 'jspdf'
    import html2canvas from 'html2canvas';  
    //Excel
    import Vue from 'vue'
    import excel from 'vue-excel-export'



    export default {
        data(){
            return {
                ingresos:[],                  
                dialog: false,
               headers: [
                               
                    { text: 'Opciones', value: 'opciones', sortable: false },
                    { text: 'Usuario', value: 'usuario' },
                    { text: 'Proveedor', value: 'proveedor' },
                    { text: 'Total Compra', value: 'total'  },
                   
                    { text: 'Fecha', value: 'fechahora', sortable: false  },

                    { text: 'Tipo Comprobante', value: 'tipocomprobante' },
                    { text: 'Serie Comprobante', value: 'seriecomprobante', sortable: false  },
                    { text: 'Número Comprobante', value: 'numerocomprobante', sortable: false  },
                    
                   
                    { text: 'Caja', value: 'caja' },
                    { text: 'Estado', value: 'estado', sortable: false  }                
                ],
                cabeceraDetalles: [
                 
                    { text: 'Borrar', value: 'borrar', sortable: false },
                    { text: 'Código', value: 'codigo'},
                    { text: 'Artículo', value: 'articulo', sortable: false },
                    { text: 'Cantidad', value: 'cantidad', sortable: false  },
                    { text: 'Precio Compra', value: 'preciocompra', sortable: false  },
                   
                    { text: 'Subtotal', value: 'subtotal', sortable: false  }              
                ],

               //variables
               search: '',
                id: '',

                detalles:[],
                detallesAux:[],

                proveedores:[],
                cajas:[],

                 //COMPROBANTE 
                comprobantes:[],
                
                listadocomprobante:[],  //array select comprobante

                idcomprobante:1,  // Id Comprobante
                tipocomprobante: '',    //descripcion  del comprobante   pendiente       
                seriecomprobante:'' ,   //serie                            pendiente
                correlativocomprobante: 0,
                tasacomprobante:0, //correlativo                pendiente
               
                comprobantetabla:false,
                comprobanteingreso:false,

                traceId:'',  
                
                idusuario:2,
                idproveedor:'',     
                idcaja:2,

                
                impuesto: 1, //impuesto estatico
                codigo:'',
                verNuevo:0, //variable para visualizar formulario
                errorArticulo:null,

                totalParcial:0, //variables de totales
                totalImpuesto:0,
                total:0,

                idPersona:'',

                cabeceraArticulos: [
                    {text: 'Seleccionar', value: 'seleccionar', sortable: false },
                    { text: 'Código', value: 'articulo'},
                    { text: 'Categoría', value: 'categoria' },
                    { text: 'Nombre ', value: 'articulo'},                    
                    { text: 'Descripción', value: 'descripcion', sortable: false },
                    { text: 'Stock', value: 'stock', sortable: false  },
                    { text: 'Precio Compra', value: 'preciocompra', sortable: false  }           
                ],


                articulos:[],  //listar articulos
                codigo:'',  //codigo del articulo
                detalles:[],   // detalla el articulo econtrado           
                

                texto:'',
                verArticulos:0,
                verDet :0,
                valida: 0,
                validaMensaje:[],
                adModal: 0,
                adAccion: 0,
                adNombre: '',
                adId: '',
                verDetalle:0,
                comprobanteModal:0  ,

                //variables comprobantes
                cliente:'',
                fecha_hora:'',
                num_documento:'',
                direccion:'',

                telefono:'',
                email:'',

               // estado:'',

                 //variables fechas

                 fechaInicio:'',
                fechaFin:'',

                json_fields: {
                    'Usuario': 'usuario',
                    'Proveedor': 'proveedor',
                    'Comprobante': 'tipoComprobante',
                    'Serie': 'serieComprobante',
                    'Número': 'numeroComprobante',
                    'Fecha Hora': 'fechaHora',
                    'Impuesto': 'impuesto',
                    'Total': 'total',
                    'Estado': 'estado',
 
                },
                
           
        
        json_meta: [
            [
                {
                    'key': 'charset',
                    'value': 'utf-8'
                }
            ]
        ],

                
                       
            }
        },


        computed: {
            calcularTotal:function(){
                var resultado=0.0;
                for(var i=0;i<this.detalles.length;i++){  //bucle para recorer todos los detalles
                    resultado=resultado+(this.detalles[i].precio*this.detalles[i].cantidad);
                }
                return resultado;
            }
        },


        watch: {
            dialog (val) {
            val || this.close()
            }
        },

       created () {
            this.listar();
            this.select();
            this.selectcomprobante(); // 1.generar listado comprobante       
        },


        methods:{

            crearPDFVentas(){
                var columns = [
                    
                    {title: "Proveedor", dataKey: "proveedor"},
                 
                    {title: "Comprobante", dataKey: "comprobante"}, 
                    {title: "Serie", dataKey: "serie"},
                    {title: "Numero", dataKey: "numero"},
                    {title: "Fecha", dataKey: "fecha"},
                    {title: "Total", dataKey: "total"},
                    {title: "Estado", dataKey: "estado"}
               
                ];
                var rows = [];

                this.ingresos.map(function(x){
                    rows.push({
                    usuario:x.usuario,
                    proveedor:x.proveedor,
                    
                    comprobante:x.tipoComprobante,
                    serie:x.serieComprobante,
                    numero:x.numeroComprobante,
                    fecha:x.fechaHora,
                    total:x.total.toFixed(2),
                    estado:x.estado
                    });
                });

                // Only pt supported (not mm or in)
                var doc = new jsPDF('p', 'pt');
                doc.autoTable(columns, rows, {
                    margin: {top: 60},
                    addPageContent: function(data) {
                        doc.text("Listado de Ventas", 40, 30);
                    }
                });
                doc.save('Compras.pdf');
            },


            crearPDF(){
                var quotes = document.getElementById('factura');
                html2canvas(quotes).then(function (canvas) {
                    var imgData = canvas.toDataURL('image/png');
                    var imgWidth = 210;
                    var pageHeight = 295;
                    var imgHeight = canvas.height * imgWidth / canvas.width;
                    var heightLeft = imgHeight;
                    var doc = new jsPDF();
                    var position = 0;

                    doc.addImage(imgData, 'PNG', 0, position, imgWidth, imgHeight);                    
                    doc.save('ComprobanteIngreso.pdf');
                });
            },

            mostrarComprobante(item){
                this.limpiar();


                this.idcomprobante=item.tipoComprobante;
                this.seriecomprobante=item.serieComprobante;
                this.correlativocomprobante=item.numeroComprobante;

                this.proveedor=item.proveedor;
                this.num_documento=item.documento;
                this.direccion=item.direccion;
             
                this.fecha_hora=item.fechaHora;


                this.impuesto=item.impuesto;
                this.listarDetalles(item.idIngreso);
                this.comprobanteModal=1;
            },
            ocultarComprobante(){
                this.comprobanteModal=0;
                this.limpiar();
            },
            // 1. mostrar detalles al select el Ingreso
             verDetalles(item){    
                this.limpiar();

                this.idcomprobante=item.tipoComprobante;
                this.seriecomprobante=item.serieComprobante;
                this.correlativocomprobante=item.numeroComprobante;

                this.comprobantetabla=false;
                this.comprobanteingreso=true;

                //me.listadocomprobante[valor].serie,
              //  this.idcomprobante=item.idComprobante;
             //   this.seriecomprobante=item.serie;
              //  this.correlativocomprobante=item.correlativo;

                this.idproveedor=item.idProveedor;
                this.impuesto=item.impuesto;
                this.listarDetalles(item.idIngreso);
                this.verNuevo=1;
                this.verDet=1;
            },
             // 2. metodo Limpiar
             limpiar(){
                this.id="";
                this.idproveedor="";
              //  this.idcomprobante="";

                this.idcomprobante=1;
                this.tipocomprobante="";
                this.seriecomprobante="";
                this.correlativocomprobante=0;
                this.listadotipocomprobante=[];
                this.impuesto="15";
                this.codigo="";
                this.detalles=[];
                this.total=0;
                this.totalImpuesto=0;
                this.totalParcial=0;
                this.verDet=0;

               // this.verDet=0;
            },

            // 3. MOSTRAR NUEVO
            mostrarNuevo(){            
                this.verNuevo=1; //
                this.comprobantetabla=true; // mostrar el listado comprobante
                this.comprobanteingreso=false;// ocultar comprobante de ingreso
                             
            },
            // 4. MOSTRAR NUEVO
            ocultarNuevo(){
                this.verNuevo=0;
                this.limpiar();
             //   this.selectcomprobante(); // 3.generar listado comprobante

            },
             // 7. seleccionar Clientes
             select(){
                let me=this;
                var proveedoresArray=[];
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.get('api/Personas/SelectProveedores',configuracion).then(function(response){
                    proveedoresArray=response.data;
                    proveedoresArray.map(function(x){
                        me.proveedores.push({text: x.nombre,value:x.idPersona});
                    });
                }).catch(function(error){
                    console.log(error);
                });
            },
             // 8. seleccionar Comprobante
             selectcomprobante(){
                let me=this;
                var comprobanteArray=[];
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.get('api/Comprobantes/Select',configuracion).then(function(response){
                    comprobanteArray=response.data;
                    comprobanteArray.map(function(x){
                        me.listadocomprobante.push({
                            text:   x.descripcion,           //text variable del array comprobante
                            value:  x.idComprobante,
                            serie:  x.serie,
                            correlativo: x.correlativo ,
                            idcodigoimpuesto: x.idCodigoImpuesto,
                            tasa : x.tasa
                                                    
                            });   
                    });
                }).catch(function(error){
                    console.log(error);
                });
            },
            //cliente
             mostrarCliente(){
                this.verArticulos=1;
            },
            //cliente
            ocultarCliente(){
                this.verArticulos=0;
            },         
            //Agregar detalle del articulo
              agregarDetalle(data = []){
               this.errorArticulo=null;
                if (this.encuentra(data['idArticulo'])){ //como viene desde el Controlador
                    this.errorArticulo="El artículo ya ha sido agregado."
                }
                else{
                    this.detalles.push(
                        {idarticulo:data['idArticulo'], //como viene desde el Controlador
                        articulo: data['nombre'],
                        cantidad:1,
                        precio:data['precioCompra'],
                        descuento:0}
                    );
                }
                
            },
            // id articulo
            encuentra(id){
                var sw=0;
                for(var i=0;i<this.detalles.length;i++){
                    if (this.detalles[i].idarticulo==id){  //idarticulo variable
                        sw=1;
                    }
                }
                return sw;
            },    
             // 10. LISTAR
            listar(){
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                let url='';
                if (!me.fechaInicio || !me.fechaFin){
                    url='api/Ingresos/Listar';
                }
                else{
                    url='api/Ingresos/ConsultaFechas/'+me.fechaInicio+'/'+me.fechaFin;
                }
                axios.get(url,configuracion).then(function(response){
                    //console.log(response);
                    me.ingresos=response.data;
                }).catch(function(error){
                    console.log(error);
                });
            },        
            // LISTAR DETALLE
            listarDetalles(id){
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.get('api/Ingresos/ListarDetalles/'+id,configuracion).then(function(response){
                    me.detalles=response.data;
                }).catch(function(error){
                    console.log(error);
                });
            },

        }        
    }
</script>


<style>
    #factura {
        padding: 20px;
        font-family: Arial, sans-serif;
        font-size: 16px ;
    }

    #logo {
        float: left;
        margin-left: 2%;
        margin-right: 2%;
    }
    #imagen {
        width: 100px;
    }

    #fact {
        font-size: 18px;
        font-weight: bold;
        text-align: center;
    }   

    #datos {
        float: left;
        margin-top: 0%;
        margin-left: 2%;
        margin-right: 2%;
        /*text-align: justify;*/
    }

    #encabezado {
        text-align: center;
        margin-left: 10px;
        margin-right: 10px;
        font-size: 16px;
    }

    section {
        clear: left;
    }

    #cliente {
        text-align: left;
    }

    #facliente {
        width: 40%;
        border-collapse: collapse;
        border-spacing: 0;
        margin-bottom: 15px;
    }

    #fa {
        color: #FFFFFF;
        font-size: 14px;
    }

    #facarticulo {
        width: 100%;
        border-collapse: collapse;
        border-spacing: 0;
        padding: 20px;
        margin-bottom: 15px;
    }

    #facarticulo thead {
        padding: 20px;
        background: #2183E3;
        text-align: center;
        border-bottom: 1px solid #FFFFFF;
    }

    #gracias {
        text-align: center;
    }
</style>


