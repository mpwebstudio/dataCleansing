function dst(url) {
  var s = document.createElement('script');
  s.type = 'text/javascript';
  s.src = url;
  document.getElementsByTagName('head')[0].appendChild(s);
}

if (typeof(data8) == 'undefined')
    data8 = function() { };

if (typeof(data8.prototype.callQueue) == 'undefined')
    data8.prototype.callQueue = new Array();

if (typeof(data8.load) == 'undefined') {
    data8.load = function(service) {
      var url = 'https://misho.co.uk/Javascript/Proxy.ashx?key=A0lE89pLA8uQaAUbFgnvMS1iphcF23AO&service=' + escape(service);
      dst(url);
    }
}

data8.prototype.dynamicInvoke = function(service, func, args, options, callback) {
  var callIndex = data8.prototype.callQueue.length;
  data8.prototype.callQueue[callIndex] = callback;
  
  <!--var url = 'https://misho.co.uk/Javascript/Handler.ashx?key=A0lE89pLA8uQaAUbFgnvMS1iphcF23AO&n=7M4Tn2RfysnulqNWnAwl0w&service=' + escape(service) + '&func=' + escape(func);-->
  var url = '{\"Status\":{\"Success\":true,\"ErrorMessage\":null,\"CreditsRemaining\":999936},\"Results\":[{\"Address\":{\"Lines\":[\"Marischal College\",\"Broad Street\",\"\",\"ABERDEEN\",\"\",\"AB10 1AB\"]},\"RawAddress\":{\"Organisation\":\"ABERDEEN CITY COUNCIL\",\"Department\":\"\",\"AddressKey\":1896312,\"OrganisationKey\":0,\"PostcodeType\":\"L\",\"BuildingNumber\":0,\"SubBuildingName\":\"\",\"BuildingName\":\"MARISCHAL COLLEGE\",\"DependentThoroughfareName\":\"\",\"DependentThoroughfareDesc\":\"\",\"ThoroughfareName\":\"BROAD\",\"ThoroughfareDesc\":\"STREET\",\"DoubleDependentLocality\":\"\",\"DependentLocality\":\"\",\"Locality\":\"ABERDEEN\",\"Postcode\":\"AB101AB\",\"Dps\":\"1A\",\"PoBox\":\"\",\"PostalCounty\":\"\",\"TraditionalCounty\":\"\",\"AdministrativeCounty\":\"\"}}]}';
  for (var i = 0; args != null && i < args.length; i++) {
    url = url + '&arg' + i + '=' + encodeURIComponent(args[i]);
  }
  
  for (var i = 0; options != null && i < options.length; i++) {
    url = url + '&opt' + i + 'name=' + encodeURIComponent(options[i].name);
    url = url + '&opt' + i + 'value=' + encodeURIComponent(options[i].value);
  }
  
  url = url + '&callback=data8.prototype.callQueue[' + callIndex + ']';
  url = url + '&rnd=' + Math.floor(Math.random() * 1000000);
  
  dst(url);
}

if (typeof(data8.option) == 'undefined') {
    data8.option = function(name, value) {
      this.name = name;
      this.value = value;
    }
}

if (typeof(data8.parseJsonDate) == 'undefined') {
    data8.parseJsonDate = function (key, value) {
        var a;
        if (typeof value === 'string') {
            a =
            /^\/Date\((-?\d+)\)\/$/.exec(value);
            if (a) {
                return new Date(+a[1]);
            }
        }
        return value;
    }
}

// JSON
if(!this.JSON){JSON={};}
(function(){function f(n){return n<10?'0'+n:n;}
if(typeof Date.prototype.toJSON!=='function'){Date.prototype.toJSON=function(key){return this.getUTCFullYear()+'-'+
f(this.getUTCMonth()+1)+'-'+
f(this.getUTCDate())+'T'+
f(this.getUTCHours())+':'+
f(this.getUTCMinutes())+':'+
f(this.getUTCSeconds())+'Z';};String.prototype.toJSON=Number.prototype.toJSON=Boolean.prototype.toJSON=function(key){return this.valueOf();};}
var cx=/[\u0000\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g,escapable=/[\\\"\x00-\x1f\x7f-\x9f\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g,gap,indent,meta={'\b':'\\b','\t':'\\t','\n':'\\n','\f':'\\f','\r':'\\r','"':'\\"','\\':'\\\\'},rep;function quote(string){escapable.lastIndex=0;return escapable.test(string)?'"'+string.replace(escapable,function(a){var c=meta[a];return typeof c==='string'?c:'\\u'+('0000'+a.charCodeAt(0).toString(16)).slice(-4);})+'"':'"'+string+'"';}
function str(key,holder){var i,k,v,length,mind=gap,partial,value=holder[key];if(value&&typeof value==='object'&&typeof value.toJSON==='function'){value=value.toJSON(key);}
if(typeof rep==='function'){value=rep.call(holder,key,value);}
switch(typeof value){case'string':return quote(value);case'number':return isFinite(value)?String(value):'null';case'boolean':case'null':return String(value);case'object':if(!value){return'null';}
gap+=indent;partial=[];if(Object.prototype.toString.apply(value)==='[object Array]'){length=value.length;for(i=0;i<length;i+=1){partial[i]=str(i,value)||'null';}
v=partial.length===0?'[]':gap?'[\n'+gap+
partial.join(',\n'+gap)+'\n'+
mind+']':'['+partial.join(',')+']';gap=mind;return v;}
if(rep&&typeof rep==='object'){length=rep.length;for(i=0;i<length;i+=1){k=rep[i];if(typeof k==='string'){v=str(k,value);if(v){partial.push(quote(k)+(gap?': ':':')+v);}}}}else{for(k in value){if(Object.hasOwnProperty.call(value,k)){v=str(k,value);if(v){partial.push(quote(k)+(gap?': ':':')+v);}}}}
v=partial.length===0?'{}':gap?'{\n'+gap+partial.join(',\n'+gap)+'\n'+
mind+'}':'{'+partial.join(',')+'}';gap=mind;return v;}}
if(typeof JSON.stringify!=='function'){JSON.stringify=function(value,replacer,space){var i;gap='';indent='';if(typeof space==='number'){for(i=0;i<space;i+=1){indent+=' ';}}else if(typeof space==='string'){indent=space;}
rep=replacer;if(replacer&&typeof replacer!=='function'&&(typeof replacer!=='object'||typeof replacer.length!=='number')){throw new Error('JSON.stringify');}
return str('',{'':value});};}
if(typeof JSON.parse!=='function'){JSON.parse=function(text,reviver){var j;function walk(holder,key){var k,v,value=holder[key];if(value&&typeof value==='object'){for(k in value){if(Object.hasOwnProperty.call(value,k)){v=walk(value,k);if(v!==undefined){value[k]=v;}else{delete value[k];}}}}
return reviver.call(holder,key,value);}
cx.lastIndex=0;if(cx.test(text)){text=text.replace(cx,function(a){return'\\u'+
('0000'+a.charCodeAt(0).toString(16)).slice(-4);});}
if(/^[\],:{}\s]*$/.test(text.replace(/\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g,'@').replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g,']').replace(/(?:^|:|,)(?:\s*\[)+/g,''))){j=eval('('+text+')');return typeof reviver==='function'?walk({'':j},''):j;}
throw new SyntaxError('JSON.parse');};}})();

// Embedded proxies
data8.addresscapture = function() { data8.apply(this); }
data8.addresscapture.prototype = new data8();
data8.addresscapture.prototype.validatepostcodesimple = function(licence,postcode,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'ValidatePostcodeSimple', [licence,postcode], null, callback); }
data8.addresscapture.prototype.validatepostcode = function(licence,postcode,options,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'ValidatePostcode', [licence,postcode], options, callback); }
data8.addresscapture.prototype.getfulladdresssimple = function(licence,postcode,building,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'GetFullAddressSimple', [licence,postcode,building], null, callback); }
data8.addresscapture.prototype.quickaddresscapturesimple = function(licence,search,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'QuickAddressCaptureSimple', [licence,search], null, callback); }
data8.addresscapture.prototype.quickaddresscapture = function(licence,search,options,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'QuickAddressCapture', [licence,search], options, callback); }
data8.addresscapture.prototype.getfulladdressdts = function(licence,postcode,building,formatter,lines,fixtowncounty,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'GetFullAddressDts', [licence,postcode,building,formatter,lines,fixtowncounty], null, callback); }
data8.addresscapture.prototype.getfulladdress = function(licence,postcode,building,options,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'GetFullAddress', [licence,postcode,building], options, callback); }
data8.addresscapture.prototype.getfullrawaddresssimple = function(licence,postcode,building,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'GetFullRawAddressSimple', [licence,postcode,building], null, callback); }
data8.addresscapture.prototype.getfullrawaddress = function(licence,postcode,building,options,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'GetFullRawAddress', [licence,postcode,building], options, callback); }
data8.addresscapture.prototype.findaddresssimple = function(licence,town,street,building,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'FindAddressSimple', [licence,town,street,building], null, callback); }
data8.addresscapture.prototype.findaddress = function(licence,town,street,building,options,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'FindAddress', [licence,town,street,building], options, callback); }
data8.addresscapture.prototype.localitiesbypostcodesimple = function(licence,postcode,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'LocalitiesByPostcodeSimple', [licence,postcode], null, callback); }
data8.addresscapture.prototype.localitiesbypostcode = function(licence,postcode,options,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'LocalitiesByPostcode', [licence,postcode], options, callback); }
data8.addresscapture.prototype.localitiesbynamesimple = function(licence,name,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'LocalitiesByNameSimple', [licence,name], null, callback); }
data8.addresscapture.prototype.localitiesbyname = function(licence,name,options,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'LocalitiesByName', [licence,name], options, callback); }
data8.addresscapture.prototype.streetsbylocalitykeysimple = function(licence,localitykey,street,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'StreetsByLocalityKeySimple', [licence,localitykey,street], null, callback); }
data8.addresscapture.prototype.streetsbylocalitykey = function(licence,localitykey,street,options,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'StreetsByLocalityKey', [licence,localitykey,street], options, callback); }
data8.addresscapture.prototype.streetsbynamesimple = function(licence,locality,street,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'StreetsByNameSimple', [licence,locality,street], null, callback); }
data8.addresscapture.prototype.streetsbyname = function(licence,locality,street,options,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'StreetsByName', [licence,locality,street], options, callback); }
data8.addresscapture.prototype.addressesbystreetkeysimple = function(licence,streetkey,building,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'AddressesByStreetKeySimple', [licence,streetkey,building], null, callback); }
data8.addresscapture.prototype.addressesbystreetkey = function(licence,streetkey,building,options,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'AddressesByStreetKey', [licence,streetkey,building], options, callback); }
data8.addresscapture.prototype.addressesbylocalitykeysimple = function(licence,localitykey,building,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'AddressesByLocalityKeySimple', [licence,localitykey,building], null, callback); }
data8.addresscapture.prototype.addressesbylocalitykey = function(licence,localitykey,building,options,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'AddressesByLocalityKey', [licence,localitykey,building], options, callback); }
data8.addresscapture.prototype.fetchrawaddresssimple = function(licence,addresskey,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'FetchRawAddressSimple', [licence,addresskey], null, callback); }
data8.addresscapture.prototype.fetchrawaddress = function(licence,addresskey,options,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'FetchRawAddress', [licence,addresskey], options, callback); }
data8.addresscapture.prototype.fetchaddresssimple = function(licence,addresskey,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'FetchAddressSimple', [licence,addresskey], null, callback); }
data8.addresscapture.prototype.fetchaddress = function(licence,addresskey,options,callback) {
	data8.prototype.dynamicInvoke.call(this, 'AddressCapture', 'FetchAddress', [licence,addresskey], options, callback); }
