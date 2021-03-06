

function isInteger(s)
{
	var i;
	for (i = 0; i < s.length; i++)
	{   
		// Check that current character is number.
		var c = s.charAt(i);
		if (((c < "0") || (c > "9"))) 
		    return false;
	}
	// All characters are numbers.
	return true;
}

function checkSubmit()
{
    if(window.document.readyState != null && window.document.readyState != 'complete')
    {
	    return false;
    }
    else
    {
	    //document.body.style.cursor = "wait";
	    return true;			
    }
}


function stripCharsInBag(s, bag)
{
	var i;
	var returnString = "";
	// Search through string's characters one by one.
	// If character is not in bag, append to returnString.
	for (i = 0; i < s.length; i++)
	{   
		var c = s.charAt(i);
		if (bag.indexOf(c) == -1) 
		    returnString += c;
	}
	return returnString;
}

function daysInFebruary (year)
{
	// February has 29 days in any year evenly divisible by four,
	// EXCEPT for centurial years which are not also divisible by 400.
	return (((year % 4 == 0) && ( (!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28 );
}
function DaysArray(n) 
{
	for (var i = 1; i <= n; i++)
	{
		this[i] = 31
		if (i==4 || i==6 || i==9 || i==11) 
		{
		    this[i] = 30
		}
		if (i==2) 
		{
		    this[i] = 29
		}
	} 
	return this
}

function isDate(dtStr){					
	if(dtStr.length==0)
		return true;	
	var daysInMonth = DaysArray(12);
	var pos1=dtStr.indexOf(dtCh);
	var pos2=dtStr.indexOf(dtCh,pos1+1);
	var strYear=dtStr.substring(0,pos1);
	var strMonth=dtStr.substring(pos1+1,pos2);
	var strDay=dtStr.substring(pos2+1);
	strYr=strYear;
	if (strDay.charAt(0)=="0" && strDay.length>1) strDay=strDay.substring(1);
	if (strMonth.charAt(0)=="0" && strMonth.length>1) strMonth=strMonth.substring(1);
	for (var i = 1; i <= 3; i++) {
		if (strYr.charAt(0)=="0" && strYr.length>1) strYr=strYr.substring(1);
	}
	month=parseInt(strMonth, 10);
	day=parseInt(strDay, 10);
	year=parseInt(strYr, 10);
	if (pos1==-1 || pos2==-1){
		return false;
	}
	if (strMonth.length<1 || month<1 || month>12){
		return false;
	}
	if (strDay.length<1 || day<1 || day>31 || (month==2 && day>daysInFebruary(year)) || day > daysInMonth[month]){
		return false;
	}
	if (strYear.length != 4 || year==0 || year<minYear || year>maxYear){
		return false;
	}
	if (dtStr.indexOf(dtCh,pos2+1)!=-1 || isInteger(stripCharsInBag(dtStr, dtCh))==false){
		return false;
	}
	return true;
}
/*------------------------------------------------*/
function ShowMessage(MsgBox)
{		
	fncDispBody();	
	if (MsgBox != null && Trim(MsgBox) != "")
	{
		alert(MsgBox);
	}				
}

	function SetFocus(ctrl) {
				var obj = document.getElementById(ctrl);
				if (obj.disabled == false ) {
					obj.focus();
				}
				return false;
	}		

function findStyleRule(styleName) {
  for (i = 0; i < document.styleSheets.length; i++) { 
    for (j = 0; j < document.styleSheets(i).rules.length; j++) {
      if (document.styleSheets(i).rules(j).selectorText == styleName) {
        return document.styleSheets(i).rules(j);
      }
    }     
  }
}


var defaultColor = "red"
//form batch Job Start  With 1Labels


//job end with alert
function showAlertBox(message){
	alert(message);
}

//job end with confirm
function showConfirmBox(message){
	//confirm(message);
	if(!confirm(message))
		window.location.href = location.pathname;
}





//Check page load status 
function checkSubmit(){
		if(window.document.readyState != null && window.document.readyState != 'complete'){
			return false;
		}
		else{
			//document.body.style.cursor = "wait";
			return true;			
		}
}

//Disable contextmenu
//document.oncontextmenu = function(e){
//	return false;
//}

//Handle the Jscript error
onerror = handleErr;
function handleErr(msg, url, l){
	return true; // never show jscript error message
}

function LTrim(str)
{
  var strg,vlen;
  strg=str;
  if (strg==null)
	strg = "";
  else{
  	vlen=strg.length;
	while((vlen>0)&&((strg.charAt(0)==String.fromCharCode(12288))||(strg.charAt(0)==String.fromCharCode(32))))
	{ 
		strg=strg.substr(1);
		vlen=strg.length;
	}
  }
  return(strg);
}

function RTrim(str)
{
  var strg,vlen;
  strg=str;
  if (strg ==null)
	strg = "";
  else{
	vlen=strg.length;
	while((vlen>0)&&((strg.charAt(vlen-1)==String.fromCharCode(12288))||(strg.charAt(vlen-1)==String.fromCharCode(32))))
	{ 
		strg=strg.substr(0,vlen-1);
		vlen=strg.length;
	}
  }
  return(strg);
}

function Trim(str){
	var strAllTrim = LTrim(str);
	strAllTrim = RTrim(strAllTrim);
	return strAllTrim;
}



function fncDispBody(){
	//oPopup.hide();
	document.body.style.display = "block";
}


/***************************************************************
Udate: 10/08/2007
Editer: Son Ky
/***********************/
// variable declarations

var reAlphabetic = /^[a-zA-Z]+$/
var reAlphanumeric = /^[a-zA-Z0-9]+$/
var reNumberLetter = /^\d+$/

/***********************
String
***********************/
var dtCh= "/";
var minYear=1900;
var maxYear=2100;
function isEmpty(s){
	return ((s == null) || (s.length == 0))
}
function isAlphabetic(s){
	if (isEmpty(s) == false)
		return reAlphabetic.test(s);
}
function isAlphaNumeric(s){
	if (isEmpty(s) == false)
		return reAlphanumeric.test(s);
}
function isNumberLetter(s){
	if (isEmpty(s) == false)
  		return reNumberLetter.test(s);
}
function isValidChar(s){
	if(isEmpty(s) == false){
		var valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
		var temp;

		for (var i = 0; i < s.length; i++){
			temp = "" + s.substring(i, i+1);
			if (valid.indexOf(temp) == "-1") return false;
		}
		return true;
	}
}
function checkLen(crtl, len){
var s = crtl.value
	if (isEmpty(s) == true)
		return true;
	
	if (s.length > len)
		return false;
	
	return true;
}
function Left(s, n){
	if (n <= 0)
		return "";
	else if (n > String(s).length)
		return s;
	else
		return String(s).substring(0,n);
}
function Right(s, n){
	if (n <= 0)
		return "";
	else if (n > String(s).length)
		return s;
	else {
		var iLen = String(s).length;
		return String(s).substring(iLen, iLen - n);
	}
}
function removeBlank(s){
	s.value = Trim(s.value);
}

/***********************
Numeric
***********************/
function isNumeric(s){
	if (isEmpty(s) == false){
		    if (isNaN(s))
			    return false;
		    else
			    return true;
	}
	 return false;
}


function compareDate(date1, date2) {
	var d1 = new Date(date1);
	var d2 = new Date(date2);
	
	var timeleft;
	timeleft = d1.getTime() - d2.getTime();

	if (parseFloat(timeleft) > 0)
		return -1;
	else if (parseFloat(timeleft) < 0)
		return 1;
	else
		return 0;
}
//**************************************
//
// is char 2 byte ?
// 
//**************************************
function checkChar1Byte(value){
    
    for (i=0; i<value.length; i++) {
        if (value.charCodeAt(i)>127) {
            //alert("This string contains character 2 byte");
            return false;
        }
    }
    return true;
}

/***********************/
//Email

function isEmail(value) {
	/*
		Email Address's Format: username@subdomain.domain
		Email Address must be include 3 part:
			part 1: username
			part 2: @
			part 3: <domainname[.domainname,...]>.<domainname>
	*/
	if (value==null || value=="")	return true;
	if (value.indexOf(" ")>=0)		return false;

	var state, code, username, domain, amountOfDot, i;
	state = 1; username=''; domain=''; amountOfDot = 0;
	for (i=0; i<value.length; i++) {
		code = value.charAt(i);
		if (state==1) {
			if (	code == "<" || code == ">" 
					|| code == "(" || code == ")"	) return false;
			else if (	code == "@"	)
				if (username == '') return false;
				else state = 3;
			username += code;
		}
		else if (state==3) {
			if (	(code >= "0" && code <= "9")
					|| (code >= "A" && code <= "Z")
					|| (code >= "a" && code <= "z")
					|| code == "_"
					|| code == "-"
				) ;
			else if (code == ".")
				if (domain == '' || domain.charAt(domain.length-1) == '.') return false;
				else amountOfDot++;
			else return false;
			domain += code;
		}
	}
	if (state != 3) return false;
	if (domain == '' || domain.charAt(domain.length-1) == '.') return false;
	if (amountOfDot <1) return false;
	return true;
}






function  checkdate(crtl){
var strDate=crtl.value
     if (strDate.length == "") return true			
     if (isNumberLetter(strDate)){	
		 if (strDate.length == 8){
				var daysInMonth = DaysArray(12);
				
				var strYear=strDate.substring(0,4);
	            var strMonth=strDate.substring(4,6);
	            var strDay=strDate.substring(6,8);
	            
	            if (strDay.charAt(0)=="0" && strDay.length>1) strDay=strDay.substring(1);
	            if (strMonth.charAt(0)=="0" && strMonth.length>1) strMonth=strMonth.substring(1);
	            for (var i = 1; i <= 3; i++) {
	                	if (strYear.charAt(0)=="0" && strYear.length>1) strYear=strYear.substring(1);
	            }
	
	            month=parseInt(strMonth, 10);
	            day=parseInt(strDay, 10);
	            year=parseInt(strYear, 10);
	            if (strMonth.length<1 || month<1 || month>12){
	                  
		            return false;
	            }
	            if (strDay.length<1 || day<1 || day>31 || (month==2 && day>daysInFebruary(year)) || day > daysInMonth[month]){
		             return false;
	            }
	            if (strYear.length != 4 || year==0 || year<minYear || year>maxYear){
	            	return false;
	            }
	            return true;
			}
			else return false;
							
	 }


}

 function ischeckAll(grdName,rNumber,NumCheck){
         
        var isAll = 1;
        var row =0;
        if (document.getElementById(grdName)){
            if(rNumber > 10){
	    	    row = document.getElementById(grdName).rows.length -1 ;
	   	    }else  row = document.getElementById(grdName).rows.length ;
	   	
        
            for (var i = 2; i <= row ; i++) {
	    	    if (i<10 ){
                     strctl = "0" + i.toString();
                 }else{
                     strctl = i.toString();
                 } 
                     var obj = document.getElementById(grdName + "_ctl" + strctl + "_chkItem");
                 if (obj!=null){
                     if (obj.checked){
                         NumCheck = NumCheck +1 ;
                        isAll = isAll + 1;
                     }
                 }
            }
                     		  
         if(NumCheck ==rNumber ){
            document.getElementById(grdName +"_ctl01_chkHead").checked = true;
         }else{
          document.getElementById(grdName +"_ctl01_chkHead").checked = false;
         }
       }
     } 
       
 function checkAll(grdName){
         var row =0;
          if (document.getElementById(grdName)){
           row = document.getElementById(grdName).rows.length ;
        
        for (var i = 2; i <= row ; i++) {
	    	if (i<10 ){
                     strctl = "0" + i.toString();
             }else{
                     strctl = i.toString();
             } 
             var obj = document.getElementById(grdName + "_ctl" + strctl + "_chkItem");
             if (obj!=null){
                if ( document.getElementById(grdName +"_ctl01_chkHead").checked)
                     obj.checked = true ;
                else
                    obj.checked = false ;              
             }
         }
         }
                       		  
    }
    function checkAllPage(grdName,Flag){
         var row =0;
          if (document.getElementById(grdName)){
           row = document.getElementById(grdName).rows.length ;
        
        for (var i = 2; i <= row ; i++) {
	    	if (i<10 ){
                     strctl = "0" + i.toString();
             }else{
                     strctl = i.toString();
             } 
             var obj = document.getElementById(grdName + "_ctl" + strctl + "_chkItem");
             if (obj!=null){
                if (Flag =="1"){
                   
                     obj.checked = true ;
                }if(Flag =="2"){
                      obj.checked = false ;
                   
                }       
             }
         }
         }
                       		  
    }
    function resetCheck(grdName){
         var row =0;
           row = document.getElementById(grdName).rows.length ;
        
        for (var i = 2; i <= row ; i++) {
	    	if (i<10 ){
                     strctl = "0" + i.toString();
             }else{
                     strctl = i.toString();
             } 
             var obj = document.getElementById(grdName + "_ctl" + strctl + "_chkItem");
             if (obj!=null){
                                      
                    obj.checked = false ;              
             }
         }
                       		  
    }

    function cursor_wait() {
        document.body.style.cursor = 'wait';
        timeoutDisableAllButton();
        
//        xInput = document.getElementsByTagName('input');
//        for (var i=0; i<xInput.length; i++){
//            if (xInput[i].type =="submit" || xInput[i].type =="button"){
//                xInput[i].disabled = true;
//            }
//        }
        return true;
    }   
    function cursor_clear() {
        document.body.style.cursor = 'default';
        return true;
    }
    
    
    function timeoutDisableAllButton(){
       // alert("timeout");
        window.setTimeout("DisableAllButton()", 0);
        return true;
    }
    
    function DisableAllButton(){
         var aButton = document.getElementsByTagName('input');
        for (var i = 0; i < aButton.length; i++){
            if ((aButton[i].type == "submit") || (aButton[i].type == "button")) {
                //alert(aButton[i].id);
                aButton[i].disabled = true;
            } 
        }
    }      
    
    
    function applyWait(){    

        cursor_clear();
        var aLink = document.getElementsByTagName('a');
        for(var i=0; i<aLink.length; i++){
            var strUrl = aLink[i].href;
            var strTarget = aLink[i].target;

            if ((strUrl !="") && strTarget !="_blank"){
                if ((strUrl.indexOf('http://',0)>=0) || (strUrl.indexOf('javascript:__doPostBack',0)>=0)){
                    aLink[i].attachEvent('onclick', cursor_wait);                    
                }
            } 
        }
        
    }
    function DoubleClick(){
         var aButton = document.getElementsByTagName('input');
        for (var i = 0; i < aButton.length; i++){
            if ((aButton[i].type == "submit") && (aButton[i].id.indexOf("ctl00_ContentPlaceHolder1_WFS_btnHTM") >= 0)) {
                aButton[i].attachEvent("onclick", DisableSubmit);
            } 
        }
    
    }      
  
    function DisableSubmit() {
                //document.forms[0].submit();
                if (event.returnValue !=false){
                    window.setTimeout("disableButton('" + window.event.srcElement.id + "')", 0);
                }    
       }

       function disableButton(buttonID) {
                document.getElementById(buttonID).disabled=true;
       }  
    //and Must be copy below block of code to the end of .aspx file to program execute
//        <script language='javascript'>
        //     applyWait();
//        </script> 

    function setAsChanged()
    {
        //Start, Duc modified: 20091209 -->Using MasterPage
        var	change = document.getElementById("ctl00_ContentPlaceHolder1_hdValueChanged");
    	//var	change = document.getElementById("hdValueChanged");
    	//End, Duc modified: 20091209
    	if (change)
    	{
	    	change.value = true;
	    }
    }
   
    function applyCheckChanged()
    {
        var xInput = document.getElementsByTagName('input');
        for(var i=0;i<xInput.length;i++)
        {
            if (xInput[i].type != 'submit' && xInput[i].type != 'button')
            {
	            xInput[i].attachEvent('onchange', setAsChanged);
	        }
        }
        var xSelect = document.getElementsByTagName('select');
        for(var i=0;i<xSelect.length;i++)
        {
	        xSelect[i].attachEvent('onchange', setAsChanged);
        }
        var xTextArea = document.getElementsByTagName('textarea');
        for(var i=0;i<xTextArea.length;i++)
        {
	        xTextArea[i].attachEvent('onchange', setAsChanged);
        }
        /* Duc del: 20091209
        var hdElement = document.createElement('input');
        hdElement.type = 'hidden';
        hdElement.value = 'false';
        hdElement.id = 'hdValueChanged';
        document.forms(0).appendChild(hdElement);
        */
    } 
    //USE for Message Error when Down load/ Upload file in ENCRYPTION Mode
    function getCorrectMessage(ErrMessage){
        try{
        var pos1 = ErrMessage.indexOf("@@",0);
        var pos2 = 0;
        if (pos1 >= 0){
            pos2 = ErrMessage.indexOf("@@", pos1 + 2);
        }
        if (pos1>= 0 && pos2> pos1){
            return ErrMessage.substring(pos1 + 2, pos2);
        } else {
            return ErrMessage;
        }
        }catch (Err)
        {
            return ErrMessage;
        }
    }

//and Must be copy below block of code to the end of .aspx file to program execute    
// <script language='javascript'> 
//       applyCheckChanged();
//</script>

// Thiết lập biến session phía server về việc có hay không xử lý dữ liệu
function Set_ProcessState(state) {
    $.ajax({
        type: "POST",
        url: "../../../BIWebService.asmx/Set_ProcessState",
        data: "{state : " + state + "}",
        contentType: "application/json;charset=utf-8"
    });
}
// Thêm ngày: 04/07/2012
function CStr(val) {
    return (val == null) ? "" : val.toString();
}