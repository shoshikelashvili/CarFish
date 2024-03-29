﻿var mymap = L.map('mapid').setView([41.670520899331464, 44.88647967551685], 13);
L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}', {
    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
    maxZoom: 18,
    id: 'mapbox/streets-v11',
    tileSize: 512,
    zoomOffset: -1,
    accessToken: 'sk.eyJ1IjoicmF0aXNob3NoaWtlbGFzaHZpbGkiLCJhIjoiY2twbjRraTZ4MG0xNDJ2bWU1aHdwczYwMiJ9.tDqMD0egXlVCvzC2kEJAKA'
}).addTo(mymap);

var marker = L.marker([41.670520899331464, 44.88647967551685]).addTo(mymap);