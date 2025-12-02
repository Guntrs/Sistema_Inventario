<template>
    <v-layout align-start>
        <v-flex>
            <v-toolbar flat color="white">

                <v-toolbar-title class="primary--text">Ingresos</v-toolbar-title>
                    <v-divider
                    class="mx-2"
                    inset
                    vertical
                    ></v-divider>
                    <v-spacer></v-spacer>
                    <v-text-field v-if="verNuevo==0" class="text-xs-center" v-model="search" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
                    <v-btn v-if="verNuevo==0" @click="listar" color="primary" dark class="mb-2">Buscar</v-btn>               
                   
                <v-spacer></v-spacer> 

                  <!--Nuevo  -->   
                <v-btn v-if="verNuevo==0" @click="mostrarNuevo"   color="primary" dark class="mb-2">Nuevo</v-btn>                              
                    

                   <!-- Seleccionar Articulo --> 
                    <v-dialog v-model="verArticulos" max-width="1000px">
                        <v-card>
                            <v-card-title>
                                <span class="headline">Seleccione un Artículo a Ingresar</span>
                            </v-card-title>


                            <!-- para buscar un Articulo --> 
                            <v-card-text>
                                <v-container grid-list-md>
                                    <v-layout wrap>
                                        <v-flex xs12 sm12 md12 lg12 xl12>

                                            <!-- Ingresar nombre de Articulo a buscar --> 
                                            <v-text-field append-icon="search" 
                                            class="text-xs-center" v-model="texto"
                                            label="Ingrese el Nombre del artículo a buscar" @keyup.enter="listarArticulo()">
                                            </v-text-field>

                                            <template>
                                              
                                                <v-data-table
                                                        :headers="cabeceraArticulos"
                                                        :items="articulos"
                                                        class="elevation-1"
                                                         > 
                                                                
                                                        <template v-slot:item="props">
                                                            <tr>                                            
                                                                <td class="justify-center layout px-0">
                                                                    
                                                                <v-icon small class="mr-2" @click="agregarDetalle(props.item)" >
                                                                     add
                                                                </v-icon>   

                                                                </td>
                                                                    <td >                                                                              
                                                                        <span class="blue--text">
                                                                            {{ props.item.codigo }}
                                                                        </span>                                                                            
                                                                    </td>

                                                            
                                                                <td>{{props.item.categoria}}</td>

                                                                <td >
                                                                <strong>
                                                                    <div >
                                                                        <span class="black--text">
                                                                            {{ props.item.nombre }}
                                                                        </span>
                                                                    </div>
                                                                </strong>                       
                                                                </td>     
                                                                
                                                                <td>{{props.item.marca}}</td> 
                                                                <td >
                                                                <strong>
                                                                    <div >
                                                                        <span class="red--text">
                                                                            Q.{{ props.item.precioCompra.toFixed(2) }}
                                                                        </span>
                                                                    </div>
                                                                </strong>                       
                                                                </td>                                                              
                                                                <td >
                                                                <strong>
                                                                    <div >
                                                                        <span class="blue--text">
                                                                            {{ props.item.stock }}
                                                                        </span>
                                                                    </div>
                                                                </strong>                       
                                                                </td>
                                                                

           
                                                            </tr>                  
                                                        </template>
                                                    
                                                        <template v-slot:no-data>
                                                        <h3>No hay artículos agregados al detalle.</h3>>
                                                        </template>

                                                </v-data-table>
                                            
                                            </template>
                                        </v-flex>
                                    </v-layout>

                                </v-container>

                            </v-card-text>

                            <!-- ocultar Articulo -->
                            <v-card-actions>
                                <v-spacer></v-spacer>

                                <v-btn @click="ocultarArticulos()" color="blue darken" text="">
                                    Aceptar
                                </v-btn>                           
                            </v-card-actions>


                        </v-card>
                    </v-dialog>
                  

                    <!-- Activar o desactivar Compra --> 
                    <!--Ventana Activar o Desactivar -->
                    <v-dialog v-model="adModal" max-width="290">
                        <v-card>
                            <v-card-title class="headline" v-if="adAccion==1">¿Activar Item?</v-card-title>
                            <v-card-title class="headline" v-if="adAccion==2">¿Anular Ingreso?</v-card-title>
                            <v-card-text>
                                Estás a punto de 
                                <span v-if="adAccion==1">Activar </span>
                                <span v-if="adAccion==2">Anular </span>
                                el ítem {{ adNombre }}
                            </v-card-text>
                            <v-card-actions>
                                <v-spacer></v-spacer>
                                <v-btn color="green darken-1" text="text" @click="activarDesactivarCerrar">
                                    Cancelar
                                </v-btn>
                                <v-btn v-if="adAccion==1" color="orange darken-4" text="text" @click="activar">
                                    Activar
                                </v-btn>
                                <v-btn v-if="adAccion==2" color="orange darken-4" text="text" @click="desactivar">
                                   Anular
                                </v-btn>
                            </v-card-actions>

                        </v-card>
                    </v-dialog>

                                                                       
            </v-toolbar>

           <!-- tabla articulo --> 
           <v-data-table
                :headers="headers"
                :items="ingresos"
                :search="search"
                class="elevation-1"
                 v-if="verNuevo==0"
            >
           
                <template v-slot:item="props">
                     <tr>
                                
                        <td class="justify-center layout px-0">
                            
                            <v-icon
                            color="black"
                                small
                                class="mr-2"
                                 @click="verDetalles(props.item)"
                                 >
                                tab
                             </v-icon>
                            
                            <template v-if="props.item.estado=='Aceptado'">
                                        <v-icon small @click="activarDesactivarMostrar(2,props.item)">
                                        block
                                        </v-icon>
                            </template>
                                                         
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
                       

                        <td>{{ props.item.impuesto }}%</td>   

                        <td >
                            <div v-if="props.item.estado=='Aceptado'">
                            <span class="blue--text">Aceptado</span>
                            </div>
                            <div v-else>
                            <span class="red--text">{{props.item.estado}}</span>
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

                                                                  
                                    <td>
                                        <v-text-field 
                                        
                                        type="number" 
                                        v-model.number="props.item.cantidad">
                                        </v-text-field>
                                    </td>  
                                    
                                    <td >
                                         <strong>
                                            <div >
                                                <span class="red--text">
                                                    Q.{{ props.item.precio.toFixed(2) }}
                                                </span>
                                            </div>
                                        </strong>                       
                                    </td>
                                    
                                                                   
                                 

                                    <td >
                                         <strong>
                                            <div >
                                                <span class="black--text">
                                                    Q. {{ props.item.cantidad * props.item.precio.toFixed(2)}}
                                                </span>
                                            </div>
                                        </strong>                       
                                    </td>
                                                          
                              
                                            
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
    export default {
        data(){
            return {
                ingresos:[],                
                dialog: false,
                headers: [
                               
                    { text: 'Opciones', value: 'opciones', sortable: false },
                    
                    { text: 'Usuario', value: 'usuario' },

                    { text: 'Proveedor', value: 'proveedor' },
                    { text: 'Total', value: 'total'  },
                    { text: 'Fecha', value: 'fechahora', sortable: false  },
                
                    { text: 'Tipo Comprobante', value: 'tipocomprobante' },
                    { text: 'Serie Comprobante', value: 'seriecomprobante', sortable: false  },
                    { text: 'Número Comprobante', value: 'numerocomprobante'  },
                    
                    { text: 'Impuesto', value: 'impuesto', sortable: false  },
                    
                    { text: 'Estado', value: 'estado'  }                
                ],
                cabeceraDetalles: [
                    { text: 'Borrar', value: 'borrar', sortable: false },
                    { text: 'Código', value: 'codigo'},
                    { text: 'Desripciòn Articulo', value: 'descripcion', sortable: false },
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

                idcomprobante:2,  // Id Comprobante
                tipocomprobante: '',    //descripcion  del comprobante   pendiente       
                seriecomprobante:'' ,   //serie                            pendiente
                correlativocomprobante: 0,
                tasacomprobante:0, //correlativo                pendiente
               
                comprobantetabla:false,
                comprobanteingreso:false,

                traceId:'',  
                
                idusuario:0,
                idproveedor:'',     
                idcaja:1,

                
                impuesto: 0.15, //impuesto estatico
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
                    { text: 'Nombre del Articulo', value: 'articulo'},  
                    { text: 'Marca ', value: 'marca'},                  
                    { text: 'Precio Compra', value: 'preciocompra', sortable: false  } ,
                    { text: 'Stock', value: 'stock', sortable: false  }
                               
                ],

                articulos:[],
                texto:'',
                verArticulos:0,

                verDet: 0, //quitar boton guardar de los detalles
                valida: 0,
                validaMensaje:[],
                adModal: 0,
                adAccion: 0,
                adNombre: '',
                adId: '',
                verDetalle:0  
                       
            }
        },


        computed: {
            calcularTotal:function(){
                var resultado=0.0;
                for(var i=0;i<this.detalles.length;i++){  //bucle para recorer todos los detalles
                    resultado=resultado+(this.detalles[i].precio*this.detalles[i].cantidad);
                }
                return resultado;
            },
            esIdUsuario(){
               
                return this.$store.state.usuario.idusuario;
                }
            


        },

                
               
  
      //  v-text=this.$store.state.usuario.nombre



        watch: {
            dialog (val) {
            val || this.close()
            }
        },



       created () {
            this.listar();          
            this.select();
            this.selectcomprobante();
          //  this.mounted();
           // this.ocultarArticulos(); // 1.generar listado comprobante

        },


        methods:{

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

             //2. metodo Limpiar
            limpiar(){
                this.id="";
                this.idproveedor="";
              //  this.idcomprobante="";

                this.idcomprobante=2;
                this.tipocomprobante="";
                this.seriecomprobante="";
                this.correlativocomprobante=0;
                this.listadotipocomprobante=[];
                this.impuesto="0.15";
                this.codigo="";
                this.detalles=[];
                this.total=0;
                this.totalImpuesto=0;
                this.totalParcial=0;
                this.verDet=0;
            },

            // 3. Mostrar Nuevo
            mostrarNuevo(){
                this.verNuevo=1;
                this.comprobantetabla=true; // mostrar el listado comprobante
                this.comprobanteingreso=false;// ocultar comprobante de ingreso
            },

            // 4. Mostrar Nuevo
            ocultarNuevo(){
                this.verNuevo=0;
                this.limpiar();
               // this.selectcomprobante();
            },
          
            
            mostrarArticulos(){
                this.verArticulos=1;
            },
            ocultarArticulos(){
                this.verArticulos=0;
               // this.articulos=[];
              //  this.texto="";
            },
            
            //5. seleccionar Proveedores
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

            //6..generar listado comprobante
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
                            serie: x.serie,
                            correlativo: x.correlativo ,
                            idcodigoimpuesto: x.idCodigoImpuesto,
                            tasa : x.tasa 

                            });   
                    });
                }).catch(function(error){
                    console.log(error);
                });
            },


            // 7. Buscar Articulo
             buscarCodigo(){
                let me=this;
                me.errorArticulo=null;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.get('api/Articulos/BuscarCodigoIngreso/'+this.codigo,configuracion)
                .then(function(response){
                   // console.log(response);
                    me.agregarDetalle(response.data);
                }).catch(function(error){
                    console.log(error);
                    me.errorArticulo='No existe el artículo';
                });
            },
            // 7.1 Agregar detalle del articulo
            agregarDetalle(data = []){
               this.errorArticulo=null;
                if (this.encuentra(data['idArticulo'])){ //como viene desde el Controlador
                    this.errorArticulo="El artículo ya ha sido agregado."
                }
                else{
                    this.detalles.push(
                        {idarticulo:data['idArticulo'], //como viene desde el Controlador
                        codigo: data['codigo'],
                        articulo: data['nombre'],
                        descripcion: data['descripcion'],
                        marca: data['marca'],
                        cantidad:1,
                        precio:data['precioCompra']}
                    );
                }
                
            },
            // 7.2  id articulo
            encuentra(id){
                var sw=0;
                for(var i=0;i<this.detalles.length;i++){
                    if (this.detalles[i].idarticulo==id){  //idarticulo variable
                        sw=1;
                    }
                }
                return sw;
            },
            //  7.3 eliminar detalle articulo
            eliminarDetalle(arr,item){   //parametros arr array
                var i= arr.indexOf(item);  // i donde se almacena el index de la ubicacion del array
                if (i!==-1){           //si existe
                    arr.splice(i,1);
                }
            },



             // 8.LISTAR EL ARTICULO
             listarArticulo(){
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.get('api/Articulos/ListarIngreso/'+me.texto,configuracion).then(function(response){
                    //console.log(response);
                     me.articulos=response.data;
                }).catch(function(error){
                    console.log(error);
                });
            },

             // Listar Ingreso
            listar(){
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                let url='';
                if(!me.search){
                    url='api/Ingresos/Listar/';
                    
                }
                else{
                    url='api/Ingresos/ListarFiltro/'+me.search;
                }
                axios.get(url,configuracion).then(function(response){
                    //console.log(response);
                    me.ingresos=response.data;
                }).catch(function(error){
                    console.log(error);
                });
            },

            // Listar Detalle de Ingreso
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


                //metodo guardar   
            guardar () {
                if (this.validar()){
                    return;
                }
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};                
                let me=this;

                var valor= (this.idcomprobante-1); 

                 // paso los datos de la tabla detalles (array detalles) a un array auxiliar para parsear los datos 
                    for(var i =0 ; i< this.detalles.length; i++){
                        this.detallesAux.push({
                            idarticulo  : parseInt(this.detalles[i].idarticulo),
                            cantidad    : parseInt(this.detalles[i].cantidad),
                            precio      : parseFloat(this.detalles[i].precio)
                        })                       
                    }   
                       
                axios.post('api/Ingresos/Crear',{
                    'idproveedor':me.idproveedor,

                    'idusuario': parseInt(me.esIdUsuario), 
                    'idcaja': me.idcaja, 

                    'idcomprobante': me.idcomprobante,
               
                    'tipocomprobante': me.listadocomprobante[valor].text,                  
                    'seriecomprobante': me.listadocomprobante[valor].serie,
                    'numerocomprobante': me.listadocomprobante[valor].correlativo,

                   // 'tipocomprobante': me.tipocomprobante,
                   // 'seriecomprobante': me.seriecomprobante,
                  //  'numerocomprobante': me.correlativocomprobante,

                    'impuesto': parseFloat( me.impuesto), 
                    'total': parseFloat( me.total),
                    'detalles':me.detallesAux
                },configuracion).then(function(response){
                    me.ocultarNuevo();
                    me.listar();
                    me.limpiar();                        
                }).catch(function(error){
                    console.log(error);
                });
            },

            //metodo        
            deleteItem (item) {
                const index = this.desserts.indexOf(item)
                confirm('Are you sure you want to delete this item?') && this.desserts.splice(index, 1)
            },      

            //metodo Validar
            validar(){
                this.valida=0;
                this.validaMensaje=[];

                if (!this.idproveedor){
                    this.validaMensaje.push("Seleccione un proveedor.");
                }
                
                if (this.detalles.length<=0){
                    this.validaMensaje.push("Ingrese al menos un artículo al detalle.");
                }
                if (this.validaMensaje.length){
                    this.valida=1;
                }
                return this.valida;
            },

            
            activarDesactivarMostrar(accion,item){
                this.adModal=1; 
                this.adNombre=item.numeroComprobante;
                //this.adNombre=item.nombre; //valor de la BD
                this.adId=item.idIngreso;  //valor de la BD              
                if (accion==1){
                    this.adAccion=1;
                }
                else if (accion==2){
                    this.adAccion=2;
                }
                else{
                    this.adModal=0;
                }
            },
            activarDesactivarCerrar(){
                this.adModal=0;
            },
            /*
            activar(){
                let me=this;
                axios.put('/api/KreaUsuarios/Activar/'+this.adId,{}).then(function(response){
                    me.adModal=0;
                    me.adAccion=0;
                    me.adNombre="";
                    me.adId="";
                    me.listar();                       
                }).catch(function(error){
                    console.log(error);
                });
            },*/
            desactivar(){
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.put('api/Ingresos/Anular/'+this.adId,{},configuracion).then(function(response){
                    me.adModal=0;
                    me.adAccion=0;
                    me.adNombre="";
                    me.adId="";
                    me.listar();                       
                }).catch(function(error){
                    console.log(error);
                });
            }

        }        
    }
</script>



