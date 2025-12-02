<template>
    <v-layout align-start>
        <v-flex>
            <v-toolbar flat color="white">
                <v-btn @click="crearPDF()"><v-icon>print</v-icon></v-btn>

                <v-btn >

                    <!-- Exportar a Excel -->
                <export-excel
                class   = "btn btn-default"
                color="primary" 
                :data   = "articulos"
                :fields = "json_fields"
                worksheet = "Listado de Arcticulos"
                name    = "Listado de Articulos.xls">
                
                Exportar a Excel
                </export-excel>
                </v-btn>
                    
                
                <v-toolbar-title class="primary--text" >Artículos</v-toolbar-title>
                    <v-divider class="mx-2" inset vertical>
                    </v-divider>

                    <v-spacer></v-spacer>
                    <v-text-field class="text-xs-center" v-model="search" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
                    <v-spacer></v-spacer> 

                    <!-- NUEVO -->
                                      
                    <v-dialog v-model="dialog" max-width="500px">

                         <template v-slot:activator="{ on }"> 
                             <v-btn color="primary" dark class="mb-2" v-on="on">Nuevo</v-btn>                              
                        </template>

                        <v-card>
                            <v-card-title>
                                <span class="headline">{{ formTitle }}</span>
                            </v-card-title>
                
                            <v-card-text>
                                <v-container grid-list-md>
                                    <v-layout wrap>
                                        <v-flex xs6 sm6 md6>
                                            <v-text-field v-model="codigo" label="Código">
                                            </v-text-field>
                                        </v-flex>

                                        <v-flex xs6 sm6 md6>
                                            <v-select 
                                            class="data.item.textColor"

                                            text: red
                                            v-model="idcategoria"
                                            :items="categorias" 
                                            label="Categoría">
                                            </v-select>
                                        </v-flex>

                                        <v-flex xs6 sm6 md6>
                                            <v-select 
                                            v-model="idmarca"
                                            :items="marcas" 
                                            label="Marca">
                                            </v-select>
                                        </v-flex>

                                        <v-flex xs12 sm12 md12 >
                                            <v-text-field v-model="nombre" label="Nombre"></v-text-field>
                                        </v-flex>

                                       
                                   
                                      
                                        
                                    
                                        <v-flex >
                                            <textarea 
                                              rows="5"
                                              max-rows="5"                                             
                                            label="Descripción"
                                            v-model="descripcion" 
                                            placeholder="Descripciòn" 
                                            style="width:390px"
                                            >
                                        </textarea>

                                        </v-flex>  

                                        <v-flex xs6 sm6 md6>        
                                            <v-text-field 
                                                v-model.date="fechavencimiento" 
                                                label="Fecha Vencimiento"> 
                                            </v-text-field>                                       
                                        </v-flex>
                                        
                                        <v-flex xs6 sm6 md6>        
                                            <v-text-field v-model.number="cantidad" label="Cantidad"> </v-text-field>                                       
                                        </v-flex>

                                        <v-flex xs6 sm6 md6>
                                            <v-select 
                                                v-model="unidad"
                                                :items="unidades" 
                                                label="Unidad">
                                            </v-select>
                                        </v-flex>

                                        <v-flex xs6 sm6 md6 class="black--text">
                                            <strong>
                                                <v-text-field v-model.number="stock" label="Stock">
                                                </v-text-field>
                                            </strong>                                            
                                        </v-flex>
                                        

                                        <v-flex xs6 sm6 md6 class="black--text"> 
                                            <strong>
                                                <v-text-field 
                                                    prefix="Q."
                                                    v-model.number="preciocompra" 
                                                    label="Precio Compra"> 

                                                </v-text-field> 
                                            </strong>                                                                                      
                                        </v-flex>

                                        <v-flex xs6 sm6 md6 class="black--text">  
                                            <strong>
                                                <v-text-field                                            
                                                    prefix="Q."
                                                    v-model.number="precioventa" 
                                                    label="Precio Venta"> 
                                                </v-text-field>   
                                            </strong>                                                                                    
                                        </v-flex>


                                        <v-flex xs6 sm6 md6>
                                            <v-select
                                                
                                                                                             
                                                v-model="idimpuesto"
                                                :items="impuestos"
                                                label="Impuesto Venta">
                                            </v-select>
                                        </v-flex>

                                       
                                        
                                        <v-flex   class="red--text" text-h6>
                                         <strong>
                                            Utilidad: 
                                        </strong>
                                       
                                        Q. {{utilidad=(calcularUtilidad).toFixed(2)}}
                                        </v-flex>

                                        <v-flex xs12 sm12 md12 v-show="valida">
                                            <div class="red--text" v-for="v in validaMensaje" :key="v" v-text="v">
                                            </div>
                                        </v-flex>   

                                    </v-layout>
                                </v-container>
                            </v-card-text>
                


                            <v-card-actions>
                                <v-spacer></v-spacer>
                                <v-btn color="primary" text @click.native="close">Cancelar</v-btn>
                                <v-btn color="primary" text @click.native="guardar">Guardar</v-btn>
                            </v-card-actions>
                        </v-card>                  
                    </v-dialog>

               
                     <!--Ventana Activar o Desactivar -->

                    <v-dialog v-model="adModal" max-width="290">
                        <v-card>
                            <v-card-title class="headline" v-if="adAccion==1">¿Activar Item?</v-card-title>
                            <v-card-title class="headline" v-if="adAccion==2">¿Desactivar Item?</v-card-title>
                            <v-card-text>
                                Estás a punto de 
                                <span v-if="adAccion==1">Activar </span>
                                <span v-if="adAccion==2">Desactivar </span>
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
                                    Desactivar
                                </v-btn>
                            </v-card-actions>

                        </v-card>
                    </v-dialog>

                </v-toolbar>
                

                <!-- Tabla Articulos -->
           <v-data-table
                :headers="headers"
                :items="articulos"
                :search="search"
                class="elevation-1"
            >               
                <template v-slot:item="props">
                        <tr>
                            <td class="justify-center layout px-0">
                                <v-icon
                                color="black"
                                small
                                class="mr-2"
                                @click="editItem(props.item)" >
                                edit
                                </v-icon>                           
                             <template v-if="props.item.condicion">
                                <v-icon
                                small
                                @click="activarDesactivarMostrar(2,props.item)">
                                block
                                </v-icon>
                             </template>
                             <template v-else>
                                <v-icon
                                mall
                                @click="activarDesactivarMostrar(1,props.item)"
                                >
                                check
                                </v-icon>
                             </template>
                            

                            
                            </td>

                            <td>{{ props.item.codigo }}</td>
                            <td >
                               
                               <div >
                                   <span class="blue--text">
                                    {{ props.item.categoria }}
                                   </span>
                               </div>
                                              
                       </td>
                            
                            <td>{{ props.item.marca }}</td>          
                         
                              
                            <td >
                               
                                    <div >
                                        <span class="black--text">
                                            {{ props.item.nombre }}
                                        </span>
                                    </div>
                                                   
                            </td>
                            
                            <td >
                                <strong >
                                    <div >
                                        <span class="black--text">
                                            Q.{{ props.item.precioCompra.toFixed(2) }}
                                        </span>
                                    </div>
                                </strong>                       
                            </td>
                            <td >
                                <strong>
                                    <div >
                                        <span class="black--text">
                                            Q.{{ props.item.precioVenta.toFixed(2) }}
                                        </span>
                                    </div>
                                </strong>                       
                            </td>
                            
                            
                            <td>{{ props.item.tasa }}%</td>
                        
                            <td >
                                <strong>
                                    <div >
                                        <span class="red--text">
                                            Q.{{ props.item.utilidad.toFixed(2) }}
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

                            <td >
                                <strong>
                                    <div >
                                        <span class="red--text">
                                            {{ props.item.fechaVencimiento  }}
                                        </span>
                                    </div>
                                </strong>                       
                            </td>
                            

                            
                                                                 
                            <td >
                                <div v-if="props.item.inventariable">
                                <span class="blue--text">Si</span>
                                </div>
                                <div v-else>
                                <span class="red--text">NO </span>
                                </div>
                            </td>

                            <td >
                                <div v-if="props.item.condicion">
                                <span class="blue--text">Activo</span>
                                </div>
                                <div v-else>
                                <span class="red--text">Inactivo</span>
                                </div>
                            </td>


                        </tr>                  
                </template> 

                <template v-slot:no-data>
                    <v-btn color="primary" @click="listar">Resetear</v-btn>
                 </template>
           </v-data-table>
            
        </v-flex>
    </v-layout>
</template>




<!-- Logica --> 


<script>

    import axios from 'axios'

    

    import jsPDF from 'jspdf'
    import autoTable from 'jspdf-autotable';

    import Vue from 'vue'
    import excel from 'vue-excel-export'

    Vue.use(excel)

    export default {
        data(){
            return {
               articulos:[],                
               dialog: false,
               headers: [
                    { text: 'Opciones', value: 'opciones', sortable: false },
                    { text: 'Código', value: 'codigo', sortable: false }, 
                    { text: 'Categoría', value: 'categoria' },
                    { text: 'Marca', value: 'marca', sortable: false  },                
                    { text: 'Nombre', value: 'nombre' },
                              
                    { text: 'Precio Compra', value: 'preciocompra'  },
                    { text: 'Precio Venta', value: 'precioventa'  },
                    { text: 'Impuesto', value: 'impuesto', sortable: false  }, 
                    { text: 'Utilidad', value: 'utilidad'  },
                    
                    
                    { text: 'Stock', value: 'stock'  },    
                    { text: 'Fecha Vencimiento', value: 'fechavencimiento'  },                                                           
                    { text: 'Inventariable', value: 'inventariable', sortable: false  },              
                    { text: 'Estado', value: 'condicion', sortable: false  }                  
                ],
               
                
                search: '',
                editedIndex: -1,
                id: '',

                
                categorias:[],
                impuestos:[],
                marcas:[],

                unidad:'',
                unidades:['mg.','gr.','kg.', 'lb.', 'onz.','ml','L.',
                          'und.','mg/und.','gr/und.','ml/und.'],
                cantidad:0.0,


                idcategoria:'',
                idimpuesto:1,
                idmarca:'',
                fechavencimiento:'',
                
                idpresentacion:'',

                codigo: '',
                nombre: '',
                descripcion: '',

                precioventa: 0,
                preciocompra: 0,
                utilidad: 0,

                stock: 0,
                
                
                impuesto:'',
                tasa:0,
   
                valida: 0,
                validaMensaje:[],
                adModal: 0,
                adAccion: 0,
                adNombre: '',
                adId: '' ,

                 
            

                json_fields: {
                    'Código': 'codigo',
                    'Categoría': 'categoria',
                    'Marca': 'marca',
                    'Nombre Articulo': 'nombre',
                    'Descripción': 'descripcion',
                    'Cantidad': 'cantidad',
                    'Unidad': 'unidad',
                    'Precio de Compra': 'precioCompra',
                    'Precio de Venta': 'precioVenta',
                    'Utilidad': 'utilidad',
                    'Stock': 'stock',
                    'Fecha Vencimiento': 'fechaVencimiento',
                    

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
            formTitle () {
                return this.editedIndex === -1 ? 'Nuevo Artículo' : 'Actualizar Artículo'
            },

            calcularUtilidad:function(){
                var resultado=0.0;
            resultado =  (this.precioventa-(this.preciocompra + (this.precioventa*0.05)));
                return resultado;
                //return resultado;
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
            this.select2();
            this.select5();

          //  this.exportExcel();
            
        },

        //metodo
        
        methods:{

           

            //Exportar Excel
            

            crearPDF(){


                var columns = [
                    {title: "Nombre", dataKey: "nombre"},
                    {title: "Código", dataKey: "codigo"}, 
                    {title: "Categoría", dataKey: "categoria"}, 
                    {title: "Stock", dataKey: "stock"},
                    {title: "Precio Venta", dataKey: "precio_venta"}
                ];
                var rows = [];

                this.articulos.map(function(x){
                    rows.push({
                    nombre:x.nombre,
                    codigo:x.codigo,
                    categoria:x.categoria,
                    stock:x.stock,
                    precio_venta:x.precioVenta.toFixed(2)
                    });
                });

                // Only pt supported (not mm or in)
                var doc = new jsPDF('p', 'pt');
                doc.autoTable(columns, rows, {
                    margin: {top: 60},
                    addPageContent: function(data) {
                        doc.text("Listado de Artículos", 40, 30);
                    }
                });
                doc.save('Lista de Articulos.pdf');
            },

            listar(){
                let me=this;  
                
                //para autorizacion  //configuracion
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                
                axios.get('api/Articulos/Listar',configuracion).then(function(response){
                    //console.log(response);
                    me.articulos=response.data;
                }).catch(function(error){
                    console.log(error);
                });
            },

            select(){
                let me=this;  
                
                //para autorizacion  //configuracion
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                
                var categoriasArray=[];
                axios.get('api/Categorias/Select', configuracion).then(function(response){
                    categoriasArray=response.data;
                    categoriasArray.map(function(x){
                        me.categorias.push({text: x.nombre,value:x.idCategoria}); //valores del Select BD
                    });
                }).catch(function(error){
                    console.log(error);
               });
            },

            select2(){
                let me=this;

                //para autorizacion  //configuracion
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                
                var categoriasArray=[];
                axios.get('api/Impuestos/Select', configuracion).then(function(response){
                    categoriasArray=response.data;
                    categoriasArray.map(function(x){
                        me.impuestos.push({text: x.tasa,value:x.idImpuesto}); //valores del Select BD
                    });
                }).catch(function(error){
                    console.log(error);
               });
            },

            select5(){
                let me=this;  
                
                //para autorizacion  //configuracion
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};

                var categoriasArray=[];
                axios.get('api/Marcas/Select',configuracion).then(function(response){
                    categoriasArray=response.data;
                    categoriasArray.map(function(x){
                        me.marcas.push({text: x.nombre, value:x.idMarca},); //valores del Select BD
                    });
                }).catch(function(error){
                    console.log(error);
               });
            },

            

            editItem (item) {
                this.id=item.idArticulo; //como viene la variable desde el BackEnd
                this.idcategoria=item.idCategoria;
                this.idimpuesto=item.idImpuesto;
                this.idmarca= item.idMarca;

                this.fechavencimiento=item.fechaVencimiento;
                this.codigo=item.codigo;
                this.nombre=item.nombre;
                this.unidad=item.unidad;
                this.cantidad=item.cantidad
                this.stock=item.stock;
                this.preciocompra=item.precioCompra;
                this.utilidad=item.utilidad;
                this.precioventa=item.precioVenta;
                this.descripcion=item.descripcion;
                this.editedIndex=1;
                this.dialog = true
            },

            deleteItem (item) {
                const index = this.desserts.indexOf(item)
                confirm('Are you sure you want to delete this item?') && this.desserts.splice(index, 1)
            },
            close () {
                this.dialog = false;
                this.limpiar();
            },
            //metodo Limpiar
            limpiar(){
                this.id="";
                this.idcategoria="";
                this.idimpuesto=1;
                this.idsucursal="";
                this.codigo="";
                this.idmarca="";
                this.nombre="";
                this.cantidad=0.0;
                this.unidad="";
                this.stock=0;
             //  this.calcularUtilidad=0;

                this.fechavencimiento="";
                this.precioventa=0;
                this.preciocompra=0;
                this.utilidad=0.0;
                this.descripcion="";
                this.editedIndex=-1;
                
            },

              //metodo guardar
              guardar () {
                if (this.validar()){
                    return;
                }   

                //para autorizacion  //configuracion
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                
                if (this.editedIndex > -1) {                    
                    //Código para editar
                    //Código para guardar
                    let me=this;
                    axios.put('api/Articulos/Actualizar',{
                        'idarticulo':me.id,
                        'idcategoria':me.idcategoria,
                        'idimpuesto':me.idimpuesto,
                        'idmarca':me.idmarca,
                    
                        'codigo':me.codigo,
                        'nombre': me.nombre,
                        'cantidad': parseFloat( me.cantidad),
                        
                        'unidad': me.unidad,                    
                        'stock':me.stock,

                        'fechavencimiento': me.fechavencimiento,

                        'precioventa':parseFloat( me.precioventa),
                        'preciocompra':parseFloat( me.preciocompra),
                        'utilidad':parseFloat(me.utilidad),
                        'descripcion': me.descripcion
                    },configuracion).then(function(response){
                        me.close();
                        me.listar();
                        me.limpiar();                        
                    }).catch(function(error){
                        console.log(error);
                    });
                } else {
                    //Código para guardar
                    let me=this;
                    axios.post('api/Articulos/Crear',{
                        'idcategoria':me.idcategoria,

                        'idimpuesto':me.idimpuesto,
                        'idmarca':me.idmarca,
                    
                        'codigo':me.codigo,
                        'nombre': me.nombre,
                        'cantidad': parseFloat( me.cantidad),
                        
                        'unidad': me.unidad,
                       
                        'stock':me.stock,
                        'precioventa':parseFloat( me.precioventa),
                        'preciocompra':parseFloat( me.preciocompra),
                        'utilidad':parseFloat( me.utilidad),
                        'descripcion': me.descripcion

                    },configuracion).then(function(response){
                        me.close();
                        me.listar();
                        me.limpiar();                        
                    }).catch(function(error){
                        console.log(error);
                    });
                }
            },


            //metodo Validar segundo cambio
            validar(){
                this.valida=0;
                this.validaMensaje=[];

                if (this.nombre.length<3 || this.nombre.length>100){
                    this.validaMensaje.push("El nombre debe tener más de 3 caracteres y menos de 100 caracteres.");
                }
                if (this.descripcion.length>256){
                    this.validaMensaje.push("La descripcion tiene mas de 256 caracteres.");
                }

                if (!this.idcategoria){
                    this.validaMensaje.push("Seleccione una categoría.");
                }
             
                
                if (this.validaMensaje.length){
                    this.valida=1;
                }
                return this.valida;
            },


            activarDesactivarMostrar(accion,item){
                this.adModal=1; 
                this.adNombre=item.nombre; //valor de la BD
                this.adId=item.idArticulo;  //valor de la BD              
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

                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                
                axios.put('/api/Articulos/Activar/'+this.adId,{},configuracion).then(function(response){
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
                axios.put('/api/Articulos/Desactivar/'+this.adId,{},configuracion).then(function(response){
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


