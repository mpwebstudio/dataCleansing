if (typeof (data8) == 'undefined') 
{ 
data8 = function () 
{ 
} 
} 
data8.postcodeLookupButton = function (a, b) 
	{ 
	this.fields = a;
	this.options = b;
	this.validate() 
	}; 
	
data8.postcodeLookupButton.prototype.getElement = function (a) 
	{ 
	var b = document.getElementById(a);
	if (!b) 
		{ 
		var c = document.getElementsByName(a);
		if (c && c.length) 
			b = c[c.length - 1] 
		} 
	return b 
	}; 
	
data8.postcodeLookupButton.prototype.validate = function () 
	{ 
	this.valid = true;
	if (!this.fields) 
		{ 
		this.valid = false;
		return 
		} 
	if (!this.options) 
		{ 
		this.valid = false;
		return 
		} 
	if (!this.options.ajaxKey) 
		{ 
		this.valid = false;
		return 
		} 
	if (!this.options.license) 
		{ 
		this.valid = false;
		return 
		} 
	for (var a = 0; a < this.fields.length; a++) 
		{ 
		if (!this.getElement(this.fields[a].element)) 
			{ 
			this.valid = false;
			return 
			} 
		} 
	for (var a = 0; a < this.fields.length; a++) 
		{ 
		if (this.fields[a].field != 'organisation' && this.fields[a].field != 'line1' && this.fields[a].field != 'line2' && this.fields[a].field != 'line3' && this.fields[a].field != 'line4' && this.fields[a].field != 'line5' && this.fields[a].field != 'line6' && this.fields[a].field != 'town' && this.fields[a].field != 'county' && this.fields[a].field != 'postcode' && this.fields[a].field != 'country') 
			{ 
			this.valid = false;
			return 
			} 
		} 
	var b = false;
	var c = false;
	for (var a = 0; a < this.fields.length && (!b || !c) ; a++) 
		{ 
		if (this.fields[a].field == 'line1') 
			c = true; 
		else if (this.fields[a].field == 'postcode') 
			b = true 
		} 
	if (!b || !c) 
		{ 
		this.valid = false;
		return 
		} 
	if (!this.options.findLabel) 
		this.options.findLabel = 'Find2'; 
	if (!this.options.okLabel) 
		this.options.okLabel = 'OK';
	if (!this.options.cancelLabel) 
		this.options.cancelLabel = 'Cancel' 
	}; 

data8.postcodeLookupButton.prototype.selectAddress = function (a) 
	{ 
	for (var b = 0; b < this.fields.length; b++) 
		{ 
		var c = this.getElement(this.fields[b].element);
		var d; 
		if (this.fields[b].field == 'organisation') 
			d = this.toProperCase(a.RawAddress.Organisation); 
		else if (this.fields[b].field == 'line1') 
			d = a.Address.Lines[0]; 
		else if (this.fields[b].field == 'line2') 
			d = a.Address.Lines[1]; 
		else if (this.fields[b].field == 'line3') 
			d = a.Address.Lines[2]; 
		else if (this.fields[b].field == 'line4') 
			d = a.Address.Lines[3]; 
		else if (this.fields[b].field == 'line5') 
			d = a.Address.Lines[4];
		else if (this.fields[b].field == 'line6') 
			d = a.Address.Lines[5]; 
		else if (this.fields[b].field == 'town') 
			d = a.Address.Lines[a.Address.Lines.length - 3]; 
		else if (this.fields[b].field == 'county') 
			d = a.Address.Lines[a.Address.Lines.length - 2]; 
		else if (this.fields[b].field == 'postcode') 
			d = a.Address.Lines[a.Address.Lines.length - 1]; 
		else 
			d = c.value; 
		c.value = d 
		} 
	}; 
	
data8.postcodeLookupButton.prototype.createButton = function (a) 
	{ 
	var b = document.createElement('input'); 
	b.type = 'button'; 
	b.value = a; 
	return b 
	}; 
	
data8.postcodeLookupButton.prototype.insertButton = function () 
	{ 
	for (var a = 0; a < this.fields.length; a++) 
		{ 
		if (this.fields[a].field == 'postcode') 
			{ 
			var b = this.getElement(this.fields[a].element); 
			jQuery(this.button).insertAfter(b); 
			break 
			} 
		} 
	}; 
	
data8.postcodeLookupButton.prototype.getLineCount = function () 
	{ 
	if (typeof (this.lineCount) == 'undefined') 
		{ 
			this.lineCount = 0; 
				for (var a = 0; a < this.fields.length; a++) 
					{ 
						if (this.fields[a].field.indexOf('line') == 0) 
							this.lineCount++; 
						else if (this.fields[a].field == 'town') 
							this.lineCount++; 
						else if (this.fields[a].field == 'county') 
							this.lineCount++ 
					} 
		} 
	return this.lineCount 
	}; 
	
data8.postcodeLookupButton.prototype.usesFixedTownCounty = function () 
	{ 
		if (typeof (this.fixedTownCounty) == 'undefined') 
			{ 
			this.fixedTownCounty = false; 
				for (var a = 0; a < this.fields.length; a++) 
					{ 
					if (this.fields[a].field == 'town' || this.fields[a].field == 'county') 
						{ 
							this.fixedTownCounty = true;
							break 
						} 
					} 
			} 
		return this.fixedTownCounty 
	}; 
	
data8.postcodeLookupButton.prototype.usesOrganisation = function () 
	{ 
		if (typeof (this.organisation) == 'undefined') 
			{ 
			this.organisation = false;
			for (var a = 0; a < this.fields.length; a++) 
				{ 
				if (this.fields[a].field == 'organisation') 
					{ 
					this.organisation = true;
					break 
					} 
				} 
			} 
		return this.organisation 
	}; 
	
data8.postcodeLookupButton.prototype.showAddressList = function (b) 
	{ 
		while (this.list.options.length > 0) 
			this.list.options[this.list.options.length - 1] = null; 
		for (var c = 0; c < b.length; c++) 
			{ 
				var d = document.createElement('option'); 
				d.text = this.getAddressText(b[c]); 
				d.address = b[c]; 
				try 
					{ 
					this.list.add(d, this.list.options[null]) 
					} 
				catch (e) 
					{ 
					this.list.add(d, null) 
					} 
			} 
		this.list.multiple = false; 
		this.list.selectedIndex = 0; 
		var g = this; 
		this.list.applySelectedAddress = function () 
			{ 
			var a = b[g.list.selectedIndex];
			g.selectAddress(a) 
			}; 
		var h;
		for (var c = 0; c < this.fields.length; c++) 
			{ 
			if (this.fields[c].field == 'postcode') 
				{ 
				h = this.getElement(this.fields[c].element); 
				break 
				} 
			} 
		var f = jQuery(h).offset();
		var i = jQuery(h).height(); 
		var j = jQuery(h).width(); 
		var k = jQuery(this.dropdown); 
		this.list.style.minWidth = j + 'px';
		k.css('left', f.left + 'px'); 
		k.css('top', (f.top + i + 4) + 'px'); 
		k.show('fast'); 
		this.list.focus() 
	}; 
	
data8.postcodeLookupButton.prototype.show = function () 
	{ 
		if (!this.valid) 
		return; 
		this.shown = true;
		if (this.button) 
			{ 
			this.button.style.display = this.buttonDisplay; 
			return 
			} 
		this.createUI(); 
		for (var g = 0; g < this.fields.length; g++) 
			{ 
			if (this.fields[g].field == 'postcode') 
				{ 
				var h = this.getElement(this.fields[g].element); 
				this.button = this.createButton(this.options.findLabel);
				this.insertButton();
				this.buttonDisplay = this.button.style.display; 
				var f = this; 
				jQuery(this.button).click(function (b) 
					{ 
					var c = new data8.addresscapture();
					var d = [{ name: 'MaxLines', value: f.getLineCount() }, { name: 'FixTownCounty', value: f.usesFixedTownCounty() }, { name: 'Formatter', value: f.usesOrganisation() ? 'NoOrganisationFormatter' : 'DefaultFormatter' }];
					c.getfulladdress(f.options.license, h.value, '', d, function (a) 
						{ 
						if (!a.Status.Success) 
							{ 
								alert(a.Status.ErrorMessage) 
							} 
						else if (a.Results.length == 0) 
							{ 
								alert('Postcode not recognised') 
							} 
						else 
							{ 
								if (a.Results.length == 1) 
									{ 
										f.selectAddress(a.Results[0]) 
									} 
								else 
									{ 
									f.showAddressList(a.Results) 
									} 
							} 
						}) 
					}) 
				} 
				else if (this.fields[g].field == 'country' && this.options.expectedCountry) 
					{ 
					var i = jQuery(this.getElement(this.fields[g].element));
					var j = i.val();
					if (j != this.options.expectedCountry) 
						this.hide(true);
					var f = this;
					i.change(function (a) 
						{ 
						if (i.val() != f.options.expectedCountry) 
						f.hide(true); 
						else if (f.shown) 
						f.show() 
						}) 
					} 
			} 
	};

data8.postcodeLookupButton.prototype.hide = function (a) 
	{ 
	if (this.button) 
		{ 
		this.button.style.display = 'none'; 
		this.shown = a 
		} 
	}; 
	
data8.postcodeLookupButton.prototype.createUI = function () 
	{ 
		this.dropdown = document.createElement('div'); 
		this.dropdown.style.position = 'absolute';
		this.dropdown.style.display = 'none';
		this.dropdown.style.backgroundColor = '#FFFFFF';
		this.dropdown.style.padding = '1px';
		document.body.appendChild(this.dropdown);
		this.list = document.createElement('select');
		this.list.size = 10;
		this.dropdown.appendChild(this.list);
		var c = document.createElement('div');
		c.style.textAlign = 'right';
		c.style.marginTop = '1em';
		this.dropdown.appendChild(c);
		var d = this.createButton(this.options.okLabel);
		c.appendChild(d);
		var g = this.createButton(this.options.cancelLabel);
		c.appendChild(g);
		var h = false;
		var f = this;
		jQuery(this.list).blur(function (a) 
			{ 
			data8PostcodeLookupListOnBlur = function () 
				{ 
					if (h) 
						{ 
						h = false;
						jQuery(f.list).focus() 
						} 
					else if (document.activeElement == null || document.activeElement != d) 
						{ 
						jQuery(f.dropdown).hide('fast') 
						} 
				}; 
			setTimeout('data8PostcodeLookupListOnBlur()', 100) 
			}).dblclick(function (a) 
				{ 
				jQuery(d).click() 
				}).click(function (a) 
					{ 
						var b = navigator.userAgent;
						if (/iPad/i.test(b) || /iPhone/i.test(b)) 
							h = true 
					}); 
		jQuery(d).click(function (a) 
			{ 
			f.list.applySelectedAddress(); 
			jQuery(f.dropdown).hide('fast') 
			}); 
		var i = document.createElement('script');
		i.type = 'text/javascript'; 
		i.src = '../javascript/loader.ashx';
		//i.src = 'https://webservices.data-8.co.uk/javascript/loader.ashx?key=' + this.options.ajaxKey + '&load=AddressCapture';
		document.getElementsByTagName('head')[0].appendChild(i) 
	};
		
data8.postcodeLookupButton.prototype.getAddressText = function (a) 
	{ 
		var b = ''; 
		if (this.usesOrganisation() && a.RawAddress.Organisation) 
			b = this.toProperCase(a.RawAddress.Organisation);
		for (var c = 0; c < a.Address.Lines.length; c++) 
			{ 
			if (a.Address.Lines[c]) 
				{ 
				if (b) 
				b = b + ', '; 
				b = b + a.Address.Lines[c] 
				} 
			} 
		return b 
	}; 

data8.postcodeLookupButton.prototype.toProperCase = function (b) 
	{ 
		return b.toLowerCase().replace(/^(.)|\s(.)/g, function (a) { return a.toUpperCase() }) 
	};