
$(document).ready(function () {
  var apiKey = 'eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6IjQzYTUzMmU4NDc1NWNiMmY5MGQ1MjczODU2MzdiNGQ5NDZmYzMzYjQwYjA1ODRkM2VkOGIwNjhmNDFjMzY5Y2E5Y2EwMzY1MGI2ZTlhODVmIn0.eyJhdWQiOiIxNzM2NCIsImp0aSI6IjQzYTUzMmU4NDc1NWNiMmY5MGQ1MjczODU2MzdiNGQ5NDZmYzMzYjQwYjA1ODRkM2VkOGIwNjhmNDFjMzY5Y2E5Y2EwMzY1MGI2ZTlhODVmIiwiaWF0IjoxNjQ3MTE4NTcxLCJuYmYiOjE2NDcxMTg1NzEsImV4cCI6MTY0OTUzNDE3MSwic3ViIjoiIiwic2NvcGVzIjpbImJhc2ljIl19.BvFcQCGjqAhrMmbiw8i8j9syUIZEyNxlBCLMz33nIX3X3tPw52oY5S_uIbYHVVENXQc9suZsKENNQYcU9Nfc4YUxFZpQ_vA2aat7NAbEI-k_R6xv9AOtR754vy7f-El-PdZk_EYya_sKA93h8alDxX_OxZjYZbsVSKgIwm4U1zWJsFKQgvw1K3DyWrTPkiVHluBJEinGeQgUtFuqg7KgWQ7Ycm3-oTGOW7c5A7PX62ofh3_LnxKrljbJVNgOGR_-E3e26gO6KuSop8k2QStefIm_ilYkteYAFDNR480OkNNGfJmxzqZh1HyU0zI0UqrwCVABafEafEXXojJuniICZw'
  var app = new Mapp({
  
    element: '#app',
    presets: {
      latlng: {
        lat: 33,
        lng: 55,
      },
      zoom: 6
    },
    apiKey
  });
  
  //جستجو روی نقشه
  // app.addSearch({
  //  counts: {
  //    geocode: 10,
  //    poi: 10,
  //  },
  //  history: true,
  //});
  
  //نقشه وکتور
  app.addVectorLayers();
  //app.addLayers();
  
  //موقعیت یاب لحضه ای
   //app.addDynamicLocation({
   //     format: 'dms',
   //     source: 'center',
   // });
	
	// منوی کلیک راست 
	// app.addContextmenu({
    //    here: true,
    //    distance: true,
    //    area: true,
    //    copy: true,
    //    share: true,
    //    static: true,
   // });
   
  //انتخاب نقطه بر اساس لت و لان
   //app.markReverseGeocode({
    //    state: {
    //        latlng: {
    //            lat: 33,
    //            lng: 55,
    //        },
    //        zoom: 16,
    //    },
    //});
  
  //مکان کاربر از طریق GPS تعیین میشود
  // app.addGeolocation({
  //      history: true,
  //      onLoad: true,
  //      onLoadCallback: function(){
  //          console.log(app.states.user.latlng);
  //     },
  // });
  
  //اضافه کردن یک نقطه روی نقشه و به دست آوردن عرض و طول جغرافیایی
   app.map.on('click', function (e) {
  
    var marker = app.addMarker({
      name: 'advanced-marker',
      latlng: {
        lat: e.latlng.lat,
        lng: e.latlng.lng,
      },
    
      icon: app.icons.red,
      popup: {
        title: {
          i18n: 'نام مکان انتخاب شده',
        },
        description: {
          i18n: 'مکان شما روی نقشه انتخاب شد',
        },
        class: 'marker-class',
        open: true,
      },
      pan: false,
      draggable: true,
      history: false,
      on: {
        click: function () {
          console.log('Click callback');
        },
        contextmenu: function () {
          console.log('Contextmenu callback');
        },
      },
	  
	  
    });
    app.showReverseGeocode({
  //  app.markReverseGeocode({
      state: {
        latlng: {
          lat: e.latlng.lat,
          lng: e.latlng.lng,
        },
        zoom: 16,
      },
    });
  })
});