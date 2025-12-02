<template>
    <v-layout align-start>
        <v-flex>
            <v-toolbar flat color="white">


                <v-toolbar-title class="primary--text">Ventas</v-toolbar-title>
                    <v-divider
                    class="mx-2"
                    inset
                    vertical
                    ></v-divider>
                    <v-spacer></v-spacer>
                    <v-text-field 
                        v-if="verNuevo==0" 
                            class="text-xs-center" 
                            v-model="search" 
                            append-icon="search" 
                            label="Búsqueda"
                            single-line hide-details>
                    </v-text-field>
                    <v-spacer></v-spacer> 

                    
                <v-btn
                v-if="verNuevo==0" 
                @click="mostrarNuevo"   
                color="primary" dark class="mb-2"
                >Nuevo</v-btn>                              
                    
                    <!--Agregar articulos al BUSCAR -->
                    <v-dialog v-model="verArticulos" max-width="1000px">
                        <v-card>
                            <v-card-title class="red--text">
                                <span  class="headline">Seleccione un Articulo</span>
                            </v-card-title>


                            <v-card-text>

                                <v-container grid-list-md>

                                    <v-layout wrap>
                                        <v-flex xs12 sm12 md12 lg12 xl12>

                                            <v-text-field append-icon="search" 
                                            class="text-xs-center" v-model="texto"
                                            label="Ingrese artículo a buscar" @keyup.enter="listarArticulo()">
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
                                                                    <strong>
                                                                        <span class="blue--text">
                                                                            {{ props.item.codigo }}
                                                                        </span>
                                                                                
                                                                    </strong>
                                                                    
                                                                        
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
                                                                                                                        
                                                                

                                                                <td >
                                                                <strong>
                                                                    <div >
                                                                        <span class="red--text">
                                                                            Q.{{ props.item.precioVenta.toFixed(2) }}
                                                                        </span>
                                                                    </div>
                                                                </strong>                       
                                                                </td>

                                                                <td >
                                                                <strong>
                                                                    <div >
                                                                        <span class="black--text">
                                                                            {{ props.item.stock }}
                                                                        </span>
                                                                    </div>
                                                                </strong>                       
                                                                </td>

                                                                <td>{{props.item.descripcion}}</td>
    
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



                            <v-card-actions>
                                <v-spacer></v-spacer>

                                <v-btn @click="ocultarArticulos()" color="blue darken" text>
                                    Cancelar
                                </v-btn>
                                
                            </v-card-actions>


                        </v-card>
                    </v-dialog>
                    
                         <!--Activar o desactivar venta -->
                    <v-dialog v-model="adModal" max-width="290">
                        <v-card>
                            <v-card-title class="headline" v-if="adAccion==1">¿Activar Item?</v-card-title>
                            <v-card-title class="headline" v-if="adAccion==2">¿Anular Venta?</v-card-title>
                            <v-card-text>
                                Estás a punto de 
                                <span v-if="adAccion==1">Activar </span>
                                <span v-if="adAccion==2">Anular la Venta </span>
                                el No.Factura {{ adNombre }}
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

                     <!--COMPROBANTE 2024 -->


                     <v-dialog v-model="comprobanteModal" max-width="1000px">
                        <v-card>
                            <v-card-text>
                                <v-btn @click="crearPDF()"><v-icon>print</v-icon></v-btn>
                                <div id="factura">

                                     <!--Encabezado 2024 -->
                                    <header>

                                        <!--LOGO -->
                                        <div id="logo">
                                          <img src="data:image/png;base64,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" id="imagen" >
                                        </div>

                                        <!--Encabezado -->
                                        <div id="datos">
                                            <p id="encabezado">
                                                <b>NOVA</b>
                                                <br>
                                                <b>FARMACIA y LIBRERIA</b>
                                                <br>Av. Cirilo Flores, Zona 4 Quetzaltenango 
                                                <br>Telefono:32829760
                                                <br>Fotocopias e Impresiones
                                            </p>
                                        </div>

                                        <!--Datos Factura -->
                                        <div id="fact">
                                            <p >
                                               Serie:  {{seriecomprobante}}<br>
                                                Número:{{correlativocomprobante}}<br>
                                               Fecha de Emisión: {{fecha_hora}}</p>
                                          
                                        </div>


                                    </header>

                                    <!--Datos Cliente -->
                                    <section>
                                        <div>
                                            <table id="facarticulo">
                                                <thead>
                                                    <td id="fa" style="text-align:left;">
                                                        <th>Datos del Cliente</th>
                                                
                                                    </td>
                                                </thead>
                                                
                                                <!--Listar Detalles 2024 -->
                                                <tbody >
                                                    <tr>
                                                        <div id="contenedor">
                                                            <div>
                                                                <tr id="fac">
                                                                    <strong>Nombre: {{cliente}}</strong><br>
                                                                    <strong>Nit:</strong> {{num_documento}}<br>
                                                                    <strong>Dirección:</strong> {{direccion}}<br>
                                                                
                                                                </tr>
                                                            </div>
                                                            <div>
                                                              
                                                                <!--columna Dos -->
                                                              
                                                            </div>
                                                        </div>
                                                                                                    
                                                       
                                                        
                                                    </tr>
                                                </tbody>

                                            </table>
                                        </div>
                                    </section>
                                    

                                       <!--Datos Producto -->
                                    <section>
                                        <div>
                                            <table id="facarticulo">
                                                <thead>
                                                    <tr id="fa">
                                                        <th>Cantidad</th>
                                                        <th>Descripciòn</th>
                                                        <th>Precio Unitario</th>
                                                        <th>desc.</th>
                                                        <th>Precio Total</th>
                                                    </tr>
                                                </thead>
                                                

                                                <!--Listar Detalles 2024 -->
                                                <tbody>
                                                    <tr v-for="det in detalles" :key="det.idVenta">
                                                        <td style="text-align:center;">{{det.cantidad}}</td>
                                                        <td>{{det.articulo}}</td>
                                                        <td style="text-align:right;">Q.{{det.precio.toFixed(2)}}</td>
                                                        <td style="text-align:right;">{{det.descuento}}</td>
                                                        <td style="text-align:right;">Q.{{(det.cantidad*det.precio-det.descuento).toFixed(2)}}</td>
                                                    </tr>
                                                </tbody>
                                           

                                                <!--Listar totales -->
                                                <tfoot>
                                                    
                                                   
                                                  
                                                    <tr>
                                                        
                                                        <th></th>
                                                        <th></th>
                                                        <th></th>
                                                        <th style="text-align:right;">TOTAL</th>
                                                        <th style="text-align:right;">Q. {{total=(calcularTotal).toFixed(2)}}</th>
                                                    </tr>
                                                </tfoot>


                                            </table>
                                        </div>
                                    </section>
                                    <br>
                                    <br>

                                    <!--Pie de pagina -->
                                    <footer>
                                        <div id="gracias">
                                            <p><b>Gracias por su compra!</b></p>
                                        </div>
                                    </footer>
                                </div>

                                <v-btn @click="ocultarComprobante()" color="blue darken-1" text>Cancelar</v-btn>
                            </v-card-text>
                        </v-card>
                    </v-dialog>
    
                                                                       
                </v-toolbar>
                
             <!--Tabla Ventas -->

           <v-data-table
                :headers="headers"
                :items="ventas"
                :search="search"
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
                        <v-icon
                        small
                        class="mr-2"
                        @click="mostrarComprobante(props.item)"
                        >
                        print
                        </v-icon>

                        <template v-if="props.item.estado=='Aceptado'">
                            <v-icon
                            small
                            @click="activarDesactivarMostrar(2,props.item)"
                            >
                            block
                            </v-icon>
                        </template>                        
                        </td> 
                            
                        <td>{{ props.item.usuario }}</td>
                        <td>{{ props.item.cliente}}</td>
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
                                      No.  {{ props.item.numeroComprobante }}
                                    </span>
                                </div>
                            </strong>                       
                        </td>
                                                       
                        <td >
                            <div v-if="props.item.estado=='Aceptado'">
                            <span class="blue--text">Aceptado</span>
                            </div>
                            <div v-else>
                            <span class="red--text">{{props.item.estado}}</span>
                            </div>
                        </td>

                        <td>{{ props.item.Uuid }}</td>


                    </tr>                  
                </template>
             
                    <template v-slot:no-data>

                        <v-btn color="primary" @click="listar">Resetear</v-btn>
                     </template>

           </v-data-table>


            
                 <!--Formulario -->
            <v-container grid-list-sm fluid class="pa-4 white" v-if="verNuevo" >
                <v-layout row wrap>
                        <!--tipo Comprobante -->
                    <v-flex xs12 sm4 md4 lg4 xl4 >
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

                    <!-- buscar Nit Cliente -->
                    <!--Buscar Articulo -->
                     <!--PENDIENDTE 2024
                     -->             
                   <!--ingresar cliente -->
                    <v-flex xs12 sm8 md8 lg8 x14>                       
                        <v-select                      
                        :readonly="comprobanteingreso" 
                        v-model="idcliente"
                        :items="clientes" 
                        label="Cliente">
                        </v-select>                       
                    </v-flex>

    
                    <!-- buscar por codigo articulo -->
                    <v-flex xs12 sm8 md8 lg8 xl8>
                        <v-text-field 
                        v-show=comprobantetabla
                        @keyup.enter="buscarCodigo()" 
                        v-model="codigo" 
                        label="Código">
                        </v-text-field>
                    </v-flex>

                     <!-- Impuesto Tasa Comprobante Seleccionado -->
                
                   
                    
                    <!-- Mostrar articulo  icono buscar-->
                    <v-flex xs12 sm2 md2 lg2 xl2>
                        <v-btn 
                        v-show=comprobantetabla
                        @click="mostrarArticulos()" 
                        small fab dark color="orange">
                            <v-icon dark>list</v-icon>
                        </v-btn>
                    </v-flex>

                        
                    <v-flex xs12 sm2 md2 lg2 xl2 v-if="errorArticulo">
                        <div class="red--text" v-text="errorArticulo">
                        </div>
                    </v-flex>
                   
                     <!--Tabla detalles del Articulo Seleccionado -->
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
                                    small class="mr-2"  @click="eliminarDetalle(detalles,props.item)">
                                        deleted
                                    </v-icon>
                                                                                                       
                                    </td>
                                    <td >
                                        <strong>
                                            <div >

                                                <span class="black--text" >
                                                    {{ props.item.stock }}
                                                </span>
                                            </div>
                                        </strong>                       
                                    </td>

                                    <td>
                                        <strong>
                                            <div >
                                                <span class="blue--text">
                                                  {{ props.item.codigo}}
                                                </span>
                                            </div>
                                        </strong> 

                                    </td>
                                    <td>
                                        <strong>
                                            <div >
                                                <span class="black--text">
                                                    {{ props.item.articulo}}
                                                </span>
                                            </div>
                                        </strong> 

                                    </td>

                                    <td>
                                        <strong>
                                            <div >
                                                <span class="black--text">                                                    
                                                    <v-text-field                                    
                                                    type="number" 
                                                    v-model.number="props.item.cantidad">
                                                    </v-text-field>
                                                </span>
                                            </div>
                                        </strong> 
                                    </td>
                                    
                                                                          
                                    <td>
                                        <strong>
                                            <div >
                                                <span class="red--text">
                                                    Q. {{ props.item.precio.toFixed(2)}}
                                                </span>
                                            </div>
                                        </strong> 

                                    </td>
                       
                                    <td>
                                        <v-col
                                            cols="6"
                                            md="6"
                                            sm="6"
                                            >
                                            <v-text-field
                                            type="number" 
                                            v-model.number="props.item.descuento">
                                            ></v-text-field>
                                            </v-col>
                                    
                                    </td>
                                    
                                    <td>
                                        <strong>
                                            <div >
                                                <span class="black--text">
                                                    Q. {{ (props.item.cantidad * props.item.precio - props.item.descuento).toFixed(2)}}
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
                            <strong>Total del Impuesto: </strong>Q. {{totalImpuesto=(total*tasaimpuesto).toFixed(2)}}
                        </v-flex>
                        <v-flex class="text-xs-right" text-h6>
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

    export default {
        data(){
            return {
                ventas:[],                
                dialog: false,
               headers: [
                               
                    { text: 'Opciones', value: 'opciones', sortable: false },
                    { text: 'Usuario', value: 'usuario' },
                    { text: 'Cliente', value: 'cliente',sortable: false },
                    { text: 'TOTAL', value: 'total'  },
                    { text: 'Fecha', value: 'fechahora' },

                    { text: 'Tipo Comprobante', value: 'tipocomprobante',sortable: false },
                    { text: 'Serie Comprobante', value: 'seriecomprobante',sortable: false  },
                    { text: 'Numero Comprobante', value: 'numerocomprobante'  },  
                    { text: 'Estado', value: 'estado', sortable: false  }, 
                    
                    { text: 'UUID', value: 'uuid', sortable: false  } 
                    
                ],
                cabeceraDetalles: [
                    { text: 'Borrar', value: 'borrar', sortable: false },
                    { text: 'Stock', value: 'stock', sortable: false },
                    { text: 'Còdigo', value: 'codigo', sortable: false },
                    { text: 'Nombre Articulo', value: 'articulo', sortable: false },
                    { text: 'Cantidad', value: 'cantidad', sortable: false  },
                    { text: 'Precio', value: 'precio', sortable: false  },
                    { text: 'Descuento', value: 'descuento', sortable: false  },
                    { text: 'Subtotal', value: 'subtotal', sortable: false  }                
                ],

                //variables
                              
                search: '',
                id: '',
                
                traceId:'',
                idusuariotest:'',
                idusuario:0,
                idcaja:1,

                //clientes
                idcliente:2,
                clientes:[                   
                ],
                numerodocumento:'',  //nit del cliente
                errorCliente:null,

              
                
                
                listadocomprobante:[],  //array select comprobante

                idcomprobante:1,  // Id Comprobante
                tipocomprobante: '',    //descripcion  del comprobante   pendiente       
                seriecomprobante:'' ,   //serie                            pendiente
                correlativocomprobante: 0,
                tasacomprobante:0, //correlativo                pendiente

                comprobantetabla:false,  // variables para el comprobante
                comprobanteingreso:false,

                impuestocomprobante:0,
                idcodigoimpuesto:1, // idcodigoimpuesto
                            //tasa tipo documetno

                impuesto: 0.05, //impuesto estatico            

                verNuevo:0, //variable para visualizar formulario
                errorArticulo:null,

                totalParcial:0, //variables de totales
                totalImpuesto:0,
                total:0,

                cabeceraArticulos: [
                    {text: 'Seleccionar', value: 'seleccionar', sortable: false },
                    
                    { text: 'Codigo', value: 'codigo' },
                    { text: 'Categoría', value: 'categoria' },
                    
                    { text: 'Nombre del Artículo', value: 'articulo'},
                    { text: 'Precio Venta', value: 'precioventa', sortable: false  },                  
                    { text: 'Stock', value: 'stock', sortable: false  },

                    { text: 'Descripción', value: 'descripcion', sortable: false }

                   
                  
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
                email:''
                       
            }
        },


        computed: {
            calcularTotal:function(){
                var resultado=0.0;
                for(var i=0;i<this.detalles.length;i++){
                    resultado=resultado+(this.detalles[i].precio*this.detalles[i].cantidad-this.detalles[i].descuento);
                }
                return resultado;
            },
            esIdUsuario(){
               
               return this.$store.state.usuario.idusuario;
               },
           

            tasaimpuesto:function(){          
             //  var valor = (this.idcomprobante-1);
                var tasa='';
             //   tasa = (this.listadocomprobante[valor].tasa)+0;
              //  this.seriecomprobante = serie;

                tasa=0.05;
               return  tasa;      
            },         
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
            crearPDF(){
                var quotes = document.getElementById('factura');
                html2canvas(quotes).then(function (canvas) {
                    var imgData = canvas.toDataURL('image/png', 1.0 );
                    var imgWidth = 210;
                    var pageHeight = 295;
                    var imgHeight = canvas.height * imgWidth / canvas.width;
                    var heightLeft = imgHeight;
                    var doc = new jsPDF();
                    var position = 0;

                    doc.addImage(imgData, 'PNG', 0, position, imgWidth, imgHeight);                    
                    doc.save('ComprobanteVenta.pdf');
                });
            },


            mostrarComprobante(item){
                this.limpiar();


                this.idcomprobante=item.tipoComprobante;
                this.seriecomprobante=item.serieComprobante;
                this.correlativocomprobante=item.numeroComprobante;

                this.cliente=item.cliente;
                this.num_documento=item.documento;
                this.direccion=item.direccion;
             
                this.fecha_hora=item.fechaHora;


                this.impuesto=item.impuesto;
                this.listarDetalles(item.idVenta);
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
                        
                this.idcliente=item.idCliente;
                this.impuesto=item.impuesto;
                this.listarDetalles(item.idVenta);
                this.verNuevo=1;
                this.verDet=1;
            },
             // 2. metodo Limpiar
             limpiar(){
                this.id="";
                this.idcliente=2;

             //   this.tipocomprobante="1";
             //   this.codigocomprobante=1;

                this.idcomprobante=1;
                this.tipocomprobante="";
                this.seriecomprobante="";
                this.correlativocomprobante=0;
                this.listadotipocomprobante=[];

                this.impuesto="0.05";
                this.codigo="";
                this.detalles=[];
                this.total=0;
                this.totalImpuesto=0;
                this.totalParcial=0;
                this.descripcion = 0;

                this.verDet=0;
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
            // 5. Articulos
            mostrarArticulos(){
                this.verArticulos=1;
            },
            ocultarArticulos(){
                this.verArticulos=0;
            },
            
             // 7. seleccionar Clientes
             select(){
                let me=this;
                var clientesArray=[];
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.get('api/Personas/SelectClientes',configuracion).then(function(response){
                    clientesArray=response.data;
                    clientesArray.map(function(x){
                        me.clientes.push({
                            text: x.nombre,
                            value:x.idPersona,
                            documento: x.numeroDocumento,
                            direccion: x.direccion
                            
                            });
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
               //-------------------------------------------------- 
            // 9..buscar cliento por Nit
            buscarCliente(){
                let me=this;
                me.errorArticulo=null;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.get('api/Personas/BuscarClienteNit/'+this.codigo,configuracion)
                .then(function(response){
                   // console.log(response);
                    me.agregarCliente(response.data);
                }).catch(function(error){
                    console.log(error);
                    me.errorArticulo='No existe el Cliente';
                });
            },
              // 9.1.metodo busqueda cliente  PENDIETE
            agregarCliente(data = []){
               this.errorcliente=null;
                if (this.encuentra(data['idCliente'])){ //como viene desde el Controlador
                    this.errorcliente="El Cliente ya ha sido agregado."
                }
                else{
                    this.clientes.push(
                        {idcliente:data['idCliente'], //como viene desde el Controlador
                        cliente: data['nombre'],
                        numerodocumento: data['numerodocumento'],
                       
                        }
                    );
                }
                
            },
             // 9.2..buscar cliento por Nit
            listarArticulo(){
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.get('api/Articulos/ListarVenta/'+me.texto,configuracion).then(function(response){
                    //console.log(response);
                     me.articulos=response.data;
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
            //-----------------------------------------------------------
            
             // 6.ARTICULO 
            // buscar codigo articulo 
            buscarCodigo(){
                let me=this;
                me.errorArticulo=null;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.get('api/Articulos/BuscarCodigoVenta/'+this.codigo,configuracion)
                .then(function(response){
                   // console.log(response);
                    me.agregarDetalle(response.data);
                }).catch(function(error){
                    console.log(error);
                    me.errorArticulo='No existe el artículo';
                });
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
                        stock: data['stock'],
                        codigo: data['codigo'],
                        articulo: data['nombre'],
                        descripcion: data['descripcion'],
                        cantidad:1,
                        precio:data['precioVenta'],
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
            //eliminar detalle articulo
            eliminarDetalle(arr,item){   //parametros arr array
                var i= arr.indexOf(item);  // i donde se almacena el index de la ubicacion del array
                if (i!==-1){           //si existe
                    arr.splice(i,1);
                }
            },

             // 10. LISTAR
            listar(){
                let me=this;
               
                axios.get('api/Ventas/Listar').then(function(response){
                    //console.log(response);
                    me.ventas=response.data;
                }).catch(function(error){
                    console.log(error);
                });
            },
           
            // LISTAR DETALLE
            listarDetalles(id){
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.get('api/Ventas/ListarDetalles/'+id,configuracion).then(function(response){
                    me.detalles=response.data;
                }).catch(function(error){
                    console.log(error);
                });
            },

        

         //metodo
           
            deleteItem (item) {
                const index = this.desserts.indexOf(item)
                confirm('Are you sure you want to delete this item?') && this.desserts.splice(index, 1)
            },

           

            //metodo guardar
             guardar () {
                if (this.validar()){
                    return;
                }
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};                
                let me=this;   

               // var valor= (this.listadocomprobante-1);  
                var valor= (this.idcomprobante-1);              
                axios.post('api/Ventas/Crear',{
                    'idcliente':me.idcliente,

                    'idusuario': parseInt(me.esIdUsuario), 
                    'idcomprobante': me.idcomprobante,
                    'idcaja': me.idcaja,

                    'tipocomprobante': me.listadocomprobante[valor].text,  
                  //  'tipocomprobante': 'Facturaprueba',
                    'seriecomprobante': me.listadocomprobante[valor].serie,
                    'numerocomprobante': me.listadocomprobante[valor].correlativo,

                  //  'impuesto': parseFloat( me.listadotipocomprobante[valor].tasa),
                    'impuesto': parseFloat( me.impuesto), 
                    'total': parseFloat( me.total),
                    'detalles':me.detalles
                },configuracion).then(function(response){
                    me.ocultarNuevo();
                    me.listar();
                    me.limpiar(); 
                    me.selectcomprobante(); // 2. generar listado comprobante 
                 //   me.listadotipocomprobante();  
                 //   me.datoscomprobante(); //volver a llenar el listado comprobante

                }).catch(function(error){
                    console.log(error);
                });
            },


            //metodo Validar
            validar(){
                this.valida=0;
                this.validaMensaje=[];

                if (!this.idcliente){
                    this.validaMensaje.push("Seleccione un cliente.");
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
                this.adId=item.idVenta;  //valor de la BD               
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
            activar(){
                let me=this;
                axios.put('/api/Usuarios/Activar/'+this.adId,{}).then(function(response){
                    me.adModal=0;
                    me.adAccion=0;
                    me.adNombre="";
                    me.adId="";
                    me.listar();                       
                }).catch(function(error){
                    console.log(error);
                });
            },
            desactivar(){
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.put('api/Ventas/Anular/'+this.adId,{},configuracion).then(function(response){
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


