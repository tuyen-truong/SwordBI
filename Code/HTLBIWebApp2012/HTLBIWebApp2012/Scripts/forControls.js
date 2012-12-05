    
//    var C_MSG_LessMin =  getResource('C_MSG_LessMin','../JScript/GetJsResourceByAJAX.aspx');//"invalid value, must be greater than or equal to ";
//    var C_MSG_GreatMax =  getResource('C_MSG_GreatMax','../JScript/GetJsResourceByAJAX.aspx');//"invalid value, must be less than or equal to ";
//    var C_MESS_InvTime = getResource('C_MESS_InvTime','../JScript/GetJsResourceByAJAX.aspx');//"invalid time!";
//    var C_MESS_InvDate = getResource('C_MESS_InvDate','../JScript/GetJsResourceByAJAX.aspx');//"invalid date!";
//    var C_MESS_InputRequied = getResource('C_MESS_InputRequied','../JScript/GetJsResourceByAJAX.aspx');//"please input data at requied fields!";
//    var C_MSG_Changed = getResource('C_MSG_Changed','../JScript/GetJsResourceByAJAX.aspx');//"Your change has not been finished. Discard your change?";
//    var C_MSG_InvValue = getResource('C_MSG_InvValue','../JScript/GetJsResourceByAJAX.aspx');//"Invalid value";
    
    //alert(C_MSG_LessMin);
 
 //Ajax to get resource file for .js file
   
 function getResource(key, file){
    var res = getfile('GET',file+"?K="+key,false);
    var value = getvalue(res, key, 0)
    return value;
  }  					

function getvalue(res,keyno,itemindex){
		var rvalue = "";
		var resstr = new String
		resstr = "";
		var start = res.indexOf("<" + keyno + ">",0);
		if (start !=-1){
 		  var end = res.indexOf("</" + keyno + ">",start) + keyno.length + 3;
		  resstr = res.substring(start,end);
		 }
		if (resstr != ""){
			var xml_doc = new ActiveXObject("Microsoft.XMLDOM");
			xml_doc.async = false;
			xml_doc.loadXML(resstr);
			if (xml_doc.documentElement != null)
				if(xml_doc.documentElement.childNodes.item(itemindex)){
					rvalue = xml_doc.documentElement.childNodes.item(itemindex).text;
				}				
		}
		return rvalue;
}

function createHttpRequest(){
      if(window.ActiveXObject){
        try {
            return new ActiveXObject("Msxml2.XMLHTTP"); //[1]'
        } catch (e) {
            try {
                return new ActiveXObject("Microsoft.XMLHTTP"); //[1]'
            } catch (e2) {
                return null;
            }
         }
    } else if(window.XMLHttpRequest){
        return new XMLHttpRequest() //[1]'
    } else {
        return null;
    }
  }
function getfile(method , fileName , async ){
    var res = ""
    var httpoj = createHttpRequest() //[1]
    httpoj.open( method , fileName , async ) //[2]
    httpoj.onreadystatechange = function()  //[4]
    { 
      if (httpoj.readyState==4)  //[5]
      {
        res  = httpoj.responseText; //[6]
      }
    }
    httpoj.send('');
    return res;
  }
  //END AJAX to load resource
    
    //Start, Duc modified 20100114
    function openUploadWindow(UploadControlName) {
	    var width = 600;
	    var height = 200;
	    var left = parseInt((screen.availWidth/2) - (width/2),10);
        var top = parseInt((screen.availHeight/2) - (height/2),10);
	    var popupFeatures = "dialogWidth=" + width + "px;";
	    popupFeatures += "dialogHeight=" + height + "px;";
	    popupFeatures += "help: No;";
	    popupFeatures += "scroll: No;";
	    popupFeatures += "status: No;";
	    popupFeatures += "dialogTop :" + top + "px;";
	    popupFeatures += "dialogLeft :" + left + "px;";
	    var objAction = document.getElementById('<%=hidActionId.ClientID %>');
	    var objTask = document.getElementById('<%=hidTaskId.ClientID %>');
	    var objTempDir = document.getElementById(UploadControlName + "_TEMP");
	    //alert(objTempDir.value);
	    var strParaAction = "";
	    var strParaTask = "";
	    var strTempDir = "";
	    if (objAction){
	        strParaAction = "&strActionId=" + objAction.value;
	    }
	    if (objTask){
	        strParaTask = "&strTaskId=" + objTask.value;
	    }
	    
	    if (objTempDir){
	        strTempDir = "&strTempDir=" + objTempDir.value;
	    }
	    //Start, Duc modified 20100114 , use MasterPage --> change
	    //var EncryptFlg = document.getElementById("hidEncrypt");
	    var EncryptFlg = document.getElementById('<%=hidEncrypt.ClientID %>');
	    //End, Duc modified 20100114
	    if (EncryptFlg){
	        if (EncryptFlg.value == "1"){
                win = showModalDialog("AXUpload_Popup.aspx?PARENTUPLOADNAME=" + UploadControlName + strTempDir +  strParaTask + strParaAction,this ,popupFeatures)         
	        }else{
	            win = showModalDialog("Upload_Popup.aspx?PARENTUPLOADNAME=" + UploadControlName + strTempDir +  strParaTask + strParaAction,this ,popupFeatures)
	        }
	    }else{
	        win = showModalDialog("Upload_Popup.aspx?PARENTUPLOADNAME=" + UploadControlName + strTempDir +  strParaTask + strParaAction,this ,popupFeatures)
	    }
	    //win = showModalDialog("Popup.aspx?PARENTUPLOADNAME=" + UploadControlName + strTempDir +  strParaTask + strParaAction,this ,popupFeatures)
    }	
    //End, Duc modified 20100114
    
    // for checkbox
    function mergeValue(name, i)
    {
        var obj = document.getElementById(name);
        if(obj)
        {
            obj.value = ""
            for(j=0; j<i; j++)
            {
                var objlist;
                objlist = document.getElementById(name + "_ITEM" + j);
                if (objlist)
                {
                    if (objlist.checked)
                    {
                        if (obj.value !="")
                        {
                            obj.value +=",";
                        }
                        obj.value += objlist.value;
                    }
                }
            }
        }
    }

    // for radiobutton
    function storeRadioValue(obj, name, i)
    {
        var objHid = document.getElementById(name);
        if(objHid)
        {
            objHid.value = obj.value;
        }
    }
    
    //Start, Duc modified 20100114 --> use MasterPage
    function getUploadValue()
    {
        var uploadField;
        upLoadField = document.getElementById("<%=hdUploadField.ClientID %>");
        var TaskIDObj = document.getElementById('<%=hidTaskId.ClientID %>');
        var TaskID = "";
        if (TaskIDObj) {TaskID = TaskIDObj.value};
        var ActionIDObj = document.getElementById('<%=hidActionId.ClientID %>');
        var ActionID = "";
        if (ActionIDObj) {ActionID = ActionIDObj.value};
        if (upLoadField){
            var uploadSplit;
            uploadSplit = upLoadField.value.split("@@");
            for(i=0; i<uploadSplit.length; i++){
                var uploadName = document.getElementById(uploadSplit[i]);
                if (uploadName){
                    
                    var WorkItemName = uploadSplit[i].substr(4,uploadSplit[i].length);
                    var uploadButton = document.getElementById(uploadSplit[i] + "_UPLOAD");
                    var spanBelong = document.getElementById(uploadSplit[i] + "_span");
                    var fileNameSplit = uploadName.value.split("@@");
                    for(j=0; j<fileNameSplit.length; j++){
                        if (spanBelong){
           	    	        var newrow = document.createElement('div');
           	    	        var a = document.createElement('a');
           	    	        a.href = 'Download.aspx?TaskId=' + TaskID + '&ActionId='+ ActionID + '&WorkItemName=' + encodeURIComponent(WorkItemName) + '&File=' + encodeURIComponent(fileNameSplit[j]);
           	    	        a.target = '_blank';
           	    	        a.innerText = fileNameSplit[j];
        	    	        var b = document.createElement('input');
	            	        b.type = 'button';
	    	                b.style.fontSize = "8.5pt";
	    	                b.value = C_BtnDeleteText; // ' delete ';
	    	                b.onclick = dosomething;
           	    	        newrow.appendChild(a);
           	    	        //if ((uploadButton)){
           	    	        if ((uploadButton) && (!uploadButton.disabled)){
           	    	            newrow.appendChild(b);
           	    	        }// else {newrow.innerHTML = fileNameSplit[j];}
		    	            spanBelong.appendChild(newrow);             			                
                        }
                    }
                }
            }
        }
    }
//    function getUploadValue()
//    {
//        var uploadField;
//        upLoadField = document.getElementById("hdUploadField");
//        var TaskIDObj = document.getElementById('hidTaskId');
//        var TaskID = "";
//        if (TaskIDObj) {TaskID = TaskIDObj.value};
//        var ActionIDObj = document.getElementById('hidActionId');
//        var ActionID = "";
//        if (ActionIDObj) {ActionID = ActionIDObj.value};
//        if (upLoadField){
//            var uploadSplit;
//            uploadSplit = upLoadField.value.split("@@");
//            for(i=0; i<uploadSplit.length; i++){
//                var uploadName = document.getElementById(uploadSplit[i]);
//                if (uploadName){
//                    
//                    var WorkItemName = uploadSplit[i].substr(4,uploadSplit[i].length);
//                    var uploadButton = document.getElementById(uploadSplit[i] + "_UPLOAD");
//                    var spanBelong = document.getElementById(uploadSplit[i] + "_span");
//                    var fileNameSplit = uploadName.value.split("@@");
//                    for(j=0; j<fileNameSplit.length; j++){
//                        if (spanBelong){
//           	    	        var newrow = document.createElement('div');
//           	    	        var a = document.createElement('a');
//           	    	        a.href = 'Download.aspx?TaskId=' + TaskID + '&ActionId='+ ActionID + '&WorkItemName=' + encodeURIComponent(WorkItemName) + '&File=' + encodeURIComponent(fileNameSplit[j]);
//           	    	        a.target = '_blank';
//           	    	        a.innerText = fileNameSplit[j];
//        	    	        var b = document.createElement('input');
//	            	        b.type = 'button';
//	    	                b.style.fontSize = "8.5pt";
//	    	                b.value = C_BtnDeleteText; // ' delete ';
//	    	                b.onclick = dosomething;
//           	    	        newrow.appendChild(a);
//           	    	        //if ((uploadButton)){
//           	    	        if ((uploadButton) && (!uploadButton.disabled)){
//           	    	            newrow.appendChild(b);
//           	    	        }// else {newrow.innerHTML = fileNameSplit[j];}
//		    	            spanBelong.appendChild(newrow);             			                
//                        }
//                    }
//                }
//            }
//        }
//    }
    //End, Duc modified 20100114
    
    function getUploadValueEncrypt(){
        var uploadField;
        upLoadField = document.getElementById("hdUploadField"); // Note: no change
        var TaskIDObj = document.getElementById('<%=hidTaskId %>');
        var TaskID = "";
        if (TaskIDObj) {TaskID = TaskIDObj.value};
        var ActionIDObj = document.getElementById('<%=hidActionId %>');
        var ActionID = "";
        if (ActionIDObj) {ActionID = ActionIDObj.value};
        if (upLoadField){
            var uploadSplit;
            uploadSplit = upLoadField.value.split("@@");
            for(i=0; i<uploadSplit.length; i++){
                var uploadName = document.getElementById(uploadSplit[i]);
                if (uploadName){
                    
                    var WorkItemName = uploadSplit[i].substr(4,uploadSplit[i].length);
                    var uploadButton = document.getElementById(uploadSplit[i] + "_UPLOAD");
                    var spanBelong = document.getElementById(uploadSplit[i] + "_span");
                    var fileNameSplit = uploadName.value.split("@@");
                    for(j=0; j<fileNameSplit.length; j++){
                        if (spanBelong){
           	    	        var newrow = document.createElement('div');
           	    	        var a = document.createElement('a');
           	    	        a.href = 'javascript:downloadEc(\'' + fileNameSplit[j] + '\',\'' + WorkItemName + '\',\'' + uploadSplit[i] + '\')';
           	    	        a.innerText = fileNameSplit[j];
        	    	        var b = document.createElement('input');
	            	        b.type = 'button';
	    	                b.style.fontSize = "8.5pt";
	    	                b.value = C_BtnDeleteText; // ' delete ';
	    	                b.onclick = dosomething;
           	    	        newrow.appendChild(a);
           	    	        //if ((uploadButton)){
           	    	        if ((uploadButton) && (!uploadButton.disabled)){
           	    	            newrow.appendChild(b);
           	    	        }// else {newrow.innerHTML = fileNameSplit[j];}
		    	            spanBelong.appendChild(newrow);             			                
                        }
                    }
                }
            }
        }
    }

    function checkCurrency(obj){
	    var value = obj.value;
	    oldValue = value;
	    var dot = false;
	    if (value.indexOf(".",0) >= 0) dot = true;
	    var key = window.event.keyCode;
	    var ch = new String
	    ch = String.fromCharCode(key);
	    if (("1234567890.-".indexOf(ch,0) <0)){
		    window.event.returnValue = false;
		    window.event.keyCode = 0;
	    }else if (ch=="." && dot){
			    window.event.returnValue = false;
			    window.event.keyCode = 0;
	    }else if (ch=="-"){
			    obj.value = - obj.value;
			    window.event.returnValue = false;
			    window.event.keyCode = 0;
	    }						
    }

	
   function checkNumeric(obj){
		var value = obj.value;
		var key = window.event.keyCode;
		var ch = new String
		ch = String.fromCharCode(key);
		if (("1234567890".indexOf(ch,0) <0)){
			window.event.returnValue = false;
			window.event.keyCode = 0;
		}						
	}
	
	function returnMaxMin(obj, min, max){
	    var value = obj.value
	    //alert(document.activeElement.id);
	    if (value!=""){
	        if (value<min) obj.value = min;
	        else if (value>max) obj.value = max;
	    }
	}
	
	//ADD 20080219 LONGTRAN - for validate Date & Time -----------------------
	function checkDateTimeValid(strValue, type, region)
	{
    	if (type=="DATE"){
    	    return thisIsDate(strValue,region);
    	}else if(type=="TIME"){
    	    return thisIsTime(strValue)
    	}else{
    	    var value = Trim(strValue);
        	var pos=value.indexOf("-");
        	//alert('region' + region);
        	//alert('pos -:' + pos);
        	if (pos < 0) {pos = value.indexOf(" ");}
       	    var strDate=value.substring(0,pos);
       	    //alert('-' + strDate + '-');
	        var strTime=value.substring(pos+1);
	        //alert('-' + strTime + '-');
	        return (thisIsDate(strDate,region) && thisIsTime(strTime))
    	}     
	}
	
	function thisIsDate(strValue, region){
	    var value = Trim(strValue);
	     if (region=="JP"){
	        return isDate(value);
	     }
	     else{
	        if (value==""){
	            return true;
	        }
	        var pos1 = value.indexOf("/");
	        var pos2 = value.indexOf("/",pos1+1);
	        var strDay = value.substring(0,pos1);
	        var strMonth = value.substring(pos1+1,pos2);
	        var strYear = value.substring(pos2+1);
            return isDate(strYear + "/" + strMonth + "/" + strDay);
	     }
	}
	
	function thisIsTime(strValue){
	    var value = Trim(strValue);
	    if (value=="" || value==":"){return true;}
	    var pos = value.indexOf(":");
	    var strHou = value.substring(0,pos);
	    var strMin = value.substring(pos+1);
	    
	    if (!(isNumeric(strHou) && isNumeric(strMin))){
            return false;
	    }else if ( -1 < strHou &&  strHou < 24 && -1 < strMin  && strMin < 60){
            return true;
        }else{ return false;}
	}

function checkValidAllTextAreaLeng()
{
    var listOfTextArea = document.getElementById('hidden_textarea_list');
    if (listOfTextArea){
        var strList = listOfTextArea.value;
        if (strList != ""){
            var arrTextArealist = strList.split("@@");
            for (var i = 0; i<arrTextArealist.length; i++){
                var objTextArea = document.getElementById(arrTextArealist[i]);
                if (objTextArea){
                	var maxLength = objTextArea.getAttribute('maxlength');
	                var currentLength = objTextArea.value.length;
	                if (currentLength > maxLength){
	    	           alert(C_MSG_LenOver);
                       objTextArea.focus();
                       event.returnValue = false;
                       return false;
    	            }
                }
            }
        }
    }
    return true;
}    
	
function checkValidAllDateTime(){
    var listOfDate = document.getElementById('hidden_date_list');
    if (listOfDate){
        var strList = listOfDate.value;
        if (strList != ""){
            var arrDatelist = strList.split("@@");
            for (var i = 0; i<arrDatelist.length; i++){
                var objDate = document.getElementById(arrDatelist[i]);
                if (objDate){
                    var region = objDate.getAttribute('RegionType');
                    if (!checkDateTimeValid(objDate.value, 'DATE', region)){
                        if(region =='JP' ){
                            var oToFocus = document.getElementById(objDate.id + '_YEA');
                        }else{
                            var oToFocus = document.getElementById(objDate.id + '_DAY');
                        }
                        alert(C_MESS_InvDate);
                        if (oToFocus){oToFocus.focus();}
                        event.returnValue = false;
                        return false;
                    }
                }
            }
        }
    }
    
    var listOfTime = document.getElementById('hidden_time_list');
    if (listOfTime){
        var strListTime = listOfTime.value;
        if (strListTime != ""){
            var arrTimelist = strListTime.split("@@");
            for (var i = 0; i<arrTimelist.length; i++){
                var objTime = document.getElementById(arrTimelist[i]);
                if (objTime){
                    var region = objTime.getAttribute('RegionType');
                    if (!checkDateTimeValid(objTime.value, 'TIME', region)){
                        var oToFocus = document.getElementById(objTime.id + '_HOU');
                        alert(C_MESS_InvTime);
                        if (oToFocus){oToFocus.focus();}
                    
                        event.returnValue = false;
                        return false;
                    }
                }
            }
        }
    }
    var listOfDateTime = document.getElementById('hidden_datetime_list');
    if (listOfDateTime){
        var strListDateTime = listOfDateTime.value;
        if (strListDateTime != ""){
            var arrDateTimelist = strListDateTime.split("@@");
            for (var i = 0; i<arrDateTimelist.length; i++){
                var objDateTime = document.getElementById(arrDateTimelist[i]);
                if (objDateTime){
                    var region = objDateTime.getAttribute('RegionType');
                    if (!checkDateTimeValid(objDateTime.value, 'DATETIME', region)){
                       if(region =='JP' ){
                            var oToFocus = document.getElementById(objDateTime.id + '_YEA');
                        }else{
                            var oToFocus = document.getElementById(objDateTime.id + '_DAY');
                        }
                        alert(C_MESS_InvDate);
                        if (oToFocus){oToFocus.focus();}
                        event.returnValue = false;
                        return false;
                    }
                }
            }
        }
    }
    return true;
}

function checkValidateAll(){
//    if (checkValidAllDateTime() && checkValidNumCurrency()){
//        alert('OK');
//    }
//    event.returnValue = false;    
//    return false;
    return (checkValidAllDateTime() && checkValidNumCurrency() && checkValidAllTextAreaLeng());
}

function checkValidNumCurrency(){
    var listOfNum = document.getElementById('hidden_number_list');
    if (listOfNum){
        var strListNum = listOfNum.value;
        if (strListNum != ""){
            var arrNumlist = strListNum.split("@@");
            for (var i = 0; i<arrNumlist.length; i++){
                var objNum = document.getElementById(arrNumlist[i]);
                if (objNum){
                    if (!isNumeric(objNum.value) && Trim(objNum.value)!=""){
                        alert(C_MSG_InvValue);
                        objNum.focus();
                        event.returnValue = false; 
                        return false;
                    }                
                    var max = objNum.getAttribute('MaxValue');
                    var min = objNum.getAttribute('MinValue');
                    if (!checkMinMax(objNum, min, max)){return false};
                }
            }
        }
    }

    var listOfCurrency = document.getElementById('hidden_currency_list');
    if (listOfCurrency){
        var strListCurrency = listOfCurrency.value;
        if (strListCurrency != ""){
            var arrCurrencylist = strListCurrency.split("@@");
            for (var i = 0; i<arrCurrencylist.length; i++){
                var objCurrency = document.getElementById(arrCurrencylist[i]);
                if (objCurrency){
                    if (!isNumeric(objCurrency.value) && Trim(objCurrency.value)!=""){
                        alert(C_MSG_InvValue);
                        objCurrency.focus();
                        event.returnValue = false; 
                        return false;
                    }
                    var max = objCurrency.getAttribute('MaxValue');
                    var min = objCurrency.getAttribute('MinValue');
                    if (!checkMinMax(objCurrency, min, max)){return false};
                }
            }
        }
    }
    return true;
}	
	
	//END ADD LONGTRAN
	
	function checkValid(obj, Type){
	    var ogrID = obj.id.substr(0,obj.id.length - 4);
	    var oActive = document.activeElement;
	    //alert(oActive.id);
	    if (oActive.id.substr(0,oActive.id.length - 4) == ogrID){return true};
	    var rCheck = true;
        objY = document.getElementById(ogrID + "_YEA");
	    objM = document.getElementById(ogrID + "_MON");
	    objD = document.getElementById(ogrID + "_DAY");
	    objH = document.getElementById(ogrID + "_HOU");
	    objMin = document.getElementById(ogrID + "_MIN");
	    //alert(objY.id);
    
	    if (Trim(objY.value + objM.value + objD.value)==""){
	        document.getElementById(ogrID).value = "";
	        return true;
	    }
	    if (Type=="JP"){
	        strDate = Trim(objY.value) + "/" + Trim(objM.value) + "/"  + Trim(objD.value);
	    }else{
	        strDate = Trim(objD.value) + "/" + Trim(objM.value) + "/"  + Trim(objY.value);
	    }
	    
	    if(oActive.id.substr(0,oActive.id.length - 4) !=ogrID){
	        rCheck = isDate(objY.value + "/" + objM.value + "/"  + objD.value);
	        if (!rCheck) {return false};
	        //alert(ogrID);
	        //alert(rCheck);
	    }
	    document.getElementById(ogrID).value = strDate;
	    if (objH && objMin){
	    //alert();
	        if (!(isNumeric(objH.value) && isNumeric(objMin.value))){
	            return false;
	        }else if ( -1 < objH.value &&  objH.value < 24 && -1 < objMin.value  && objMin.value < 60){
	            document.getElementById(ogrID).value += "-" + Trim(objH.value) + ":" + Trim(objMin.value);
	            return true;
	        }else{ return false;}
	        
	    }
	    return rCheck;
	}

   function checkValidTime(obj, HouID, MinId){
	    var ogrID = obj.id.substr(0,obj.id.length - 4);
	    var oActive = document.activeElement;
	    objH = document.getElementById(HouID);
	    objM = document.getElementById(MinId);
        if (objH && objM){
            if (Trim(objH.value)==""){objH.value = '00'};
            if (Trim(objM.value)==""){objM.value = '00'};
            if (objH.value<0) {objH.value = '00'};
            if (objH.value > 23 ) {objH.value = 23};
	        if (objM.value<0) {objM.value = '00'};
            if (objM.value > 59 ) {objM.value = 59};
            if (objH.value.length == 1) objH.value = '0' + objH.value;
            if (objM.value.length == 1) objM.value = '0' + objM.value;

        }
        if (oActive.id==HouID || oActive.id==MinId) {return true};
	    if (!(isNumeric(objH.value) && isNumeric(objM.value))){
//	        alert(C_MESS_InvTime);
//	        obj.focus();
//	        event.returnValue = false;         
//	        return false;
	    }
	    document.getElementById(ogrID).value = Trim(objH.value) + ":" + Trim(objM.value);
	}        

    function CheckPrevValue(Obj, oYea, oMon, oDay, oType){
	    objY = document.getElementById(oYea);
	    objM = document.getElementById(oMon);
	    objD = document.getElementById(oDay);
	    return true;
	    //alert (window.event.keyCode);
	    //if (window.event.keyCode==9) {return true};
	    if (oType=="JP"){
	        if (Obj.id == oMon){
	            if (Trim(objY.value) == ""){
	                objY.focus();
	            }
	        }else if (Obj.id == oDay){
	            if (Trim(objM.value) == ""){
	                objM.focus();
	            }
	        }
	    }else{
	        if (Obj.id == oMon){
	            if (Trim(objD.value) == ""){
	                objD.focus();
	            }
	        }else if (Obj.id == oYea){
	            if (Trim(objM.value) == ""){
	                objM.focus();
	            }
	        }
	    } 
	}
	
    function GetMaxDayInMonth(YeaID,MonID){
        month=parseInt(oMon.value, 10);
	    year=parseInt(oYea.value, 10);
	    if (isNaN(month)) {return 31};
	    if (month==1 || month==3 || month==5 || month==7 || month==8 || month==10 || month==12) {return 31};
	    if (month==4 || month==6 || month==9 || month==11) {return 30};
	    if (isNaN(year)) {return 31};
	    if (month==2) {return daysInFebruary (year)};
	    return 31;
    }
	
    function dateValidRetun(obj, YeaID, MonID, DayID, Type){
        var ogrID = obj.id.substr(0,obj.id.length - 4);
	    objH = document.getElementById(ogrID + "_HOU");
	    objMin = document.getElementById(ogrID + "_MIN");
    
        oYea = document.getElementById(YeaID);
        oMon = document.getElementById(MonID);        
        oDay = document.getElementById(DayID);
        //alert();        
	    if (obj.id == YeaID){
	        if (Trim(obj.value) == ""){
	            oMon.value = "";
	            oDay.value = "";
	        }else{
	            if (obj.value < minYear) {obj.value = minYear};
	            if (obj.value > maxYear) {obj.value = maxYear};
   	            if (oDay.value !="" && oDay.value>GetMaxDayInMonth(oYea,oMon)){
	                oDay.value =GetMaxDayInMonth(oYea,oMon) ;
                }
	        }
	    }else if (obj.id == MonID){
	        if (Trim(obj.value) == ""){
	            oDay.value = "";
	        }else{
   	            if (obj.value<1) {obj.value = 1};
	            if (obj.value>12) {obj.value = 12};
	            if (obj.value.length == 1) obj.value = '0' + obj.value;
   	            if (oDay.value !="" && oDay.value>GetMaxDayInMonth(oYea,oMon)){
	                oDay.value =GetMaxDayInMonth(oYea,oMon) ;
                }
	        }    
	    }else if ((obj.id == DayID) && (Trim(obj.value) != "")){
	        if (obj.value<1) {obj.value = 1};
	        if (obj.value > GetMaxDayInMonth(oYea,oMon)) {obj.value = GetMaxDayInMonth(oYea,oMon)};
	        if (obj.value.length == 1) obj.value = '0' + obj.value;
	    }else if (obj.id == ogrID + "_HOU"){
	        if (Trim(obj.value)=="") {obj.value = '00'};
	        if (obj.value<=0) {obj.value = '00'};
	        if (obj.value > 23 ) {obj.value = 23};
	        if (obj.value.length == 1) obj.value = '0' + obj.value;
	    }else if (obj.id == ogrID + "_MIN"){
	        if (Trim(obj.value)=="") {obj.value = '00'};
	        if (obj.value<=0) {obj.value = '00'};
	        if (obj.value > 59 ) {obj.value = 59};
	        if (obj.value.length == 1) obj.value = '0' + obj.value;
	    }	    
	    if (document.getElementById(ogrID)){
	        if(Type == 'JP'){
	            document.getElementById(ogrID).value = oYea.value + '/' + oMon.value + '/'  + oDay.value;   
	        }else{
	            document.getElementById(ogrID).value = oDay.value  + '/' + oMon.value + '/' + oYea.value;
	        }
            if (oYea.value=="" && oMon.value == "" && oDay.value ==""){
                document.getElementById(ogrID).value = ""
            }
	        if  (objH && objMin){
	            var sperate = "";
	            if (Trim(document.getElementById(ogrID).value) != "") {sperate = "-"} 
	            document.getElementById(ogrID).value += sperate + objH.value + ":" + objMin.value;
            }
        }
	    
//   	    if (!checkValid(obj, Type)){
//	        alert(C_MESS_InvDate);
//	        obj.focus();
//            event.returnValue = false;
//            return false;
//	    } 
    }
    
    function CheckEnoungValue(Obj, oYea, oMon, oDay, oType){
        //Add 20080218 LONGTRAN
        var key = window.event.keyCode;
	    var ch = new String
	    ch = String.fromCharCode(key);
	   //alert(ch);
	    if (("1234567890".indexOf(ch,0) <0)){
	        return true;
	    }
	    objY = document.getElementById(oYea);
	    objM = document.getElementById(oMon);
	    objD = document.getElementById(oDay);
	    if (oType=="JP"){
	        if (Obj.id == oYea){
	            if (objY.value.length == 4){
	                objM.focus();
	            }
	        }else if (Obj.id == oMon){
	            if (objM.value.length == 2){
	                objD.focus();
	            }
	        }
	    }else{
	        if (Obj.id == oDay){
	            if (objD.value.length == 2){
	                objM.focus();
	            }
	        }else if (Obj.id == oMon){
	            if (objM.value.length == 2){
	                objY.focus();
	            }
	        }
	    } 
	}

	function checkMinMax(obj,min, max){
		var value = parseInt(obj.value,10);
		if (!isNumeric(value)){obj.value = '';}
		if (value !="" || value != null ) { 
		    if (value < min){
			    alert(C_MSG_LessMin + min)
    		    obj.focus();
                event.returnValue = false;
                return false;
       		}
	    	if (value > max){
		    	alert(C_MSG_GreatMax + max)
			    obj.focus();
                event.returnValue = false;
                return false;
		    }
		}
		return true;
	}	

    
    function checkRequiedValue(){
        var objUploadList = document.getElementById("hidden_required_upload_list");
        if(objUploadList){
            if (objUploadList.getAttribute('DISPLAY') != 'false'){ 
                if (!checRequiredUpload(objUploadList.value)){return false;}
            }
        }

        var obj = document.getElementById("hidden_required_list");
        if(obj){
            if (obj.getAttribute('DISPLAY') != 'false'){ 
                if (!checkRequiedFromString(obj.value)){return false;}
            }
        }
        //*ADD 20080223 LONGTRAN check required for Upload, checkbox & Raido
        var objCheckboxList = document.getElementById("hidden_required_checkbox_list");
        if(objCheckboxList){
            if (objCheckboxList.getAttribute('DISPLAY') != 'false'){ 
                if (!checkRequiredCheckBox(objCheckboxList.value)){return false;}
            }
        }
        var objRadioList = document.getElementById("hidden_required_radio_list");
        if(objRadioList){
            if (objRadioList.getAttribute('DISPLAY') != 'false'){ 
               if (!checkRequiredRadio(objRadioList.value)){return false;}
            }
        }
        return true;
    }
    //*ADD 20080223 LONGTRAN check required for Upload, checkbox & Raido
    function checRequiredUpload(str){
        var arrControlName = str.split("@@");
        for(i=0; i<arrControlName.length; i++){
            var control = document.getElementById(arrControlName[i]);
            var oNewValue = document.getElementById(arrControlName[i] + '_NEW');
            var oButtonUpload = document.getElementById(arrControlName[i] + '_UPLOAD');
            if (control && oNewValue){
                if ((Trim(control.value) + Trim(oNewValue.value))== ""){
                    //control.value = "";
                    alert(C_MESS_InputRequied);
                    if (oButtonUpload){
                        oButtonUpload.focus();    
                    }
                    event.returnValue = false;
                    return false;
                }
            }
        }
        return true;
    }
    function checkRequiredRadio(str){
        var arrControlName = str.split("@@");
        for(i=0; i<arrControlName.length; i++){
            var control = document.getElementById(arrControlName[i]);
            if (control){
                if (Trim(control.value) == ""){
                    //control.value = "";
                    alert(C_MESS_InputRequied);
                    var objFocus = document.getElementById(arrControlName[i].substr(0,arrControlName[i].length - 4) + '_ITEM0');
                    //obj.id.substr(0,obj.id.length - 4)
                    //alert(objFocus.id);
                    if (objFocus){
                        objFocus.focus();    
                    }
                    event.returnValue = false;
                    return false;
                }
            }
        }
        return true;
    }
    function checkRequiredCheckBox(str){
        var arrControlName = str.split("@@");
        for(i=0; i<arrControlName.length; i++){
            var control = document.getElementById(arrControlName[i]);
            if (control){
                if (Trim(control.value) == ""){
                    //control.value = "";
                    alert(C_MESS_InputRequied);
                    var objFocus = document.getElementById(arrControlName[i] + '_ITEM0');
                    if (objFocus){
                        objFocus.focus();    
                    }
                    event.returnValue = false;
                    return false;
                }
            }
        }
        return true;
    }
    //--------------------------------------------
    function checkRequiedFromString(str){
        //alert(str);
        var arrControlName = str.split("@@");
        for(i=0; i<arrControlName.length; i++){
            var control = document.getElementById(arrControlName[i]);
            //alert(control.id);
            if (control){
            //alert(control.value);
                if (Trim(control.value) == ""){
                    control.value = "";
                    alert(C_MESS_InputRequied);
                    control.focus();
                    event.returnValue = false;
                    return false;
                }
            }
        }
        return true;
    }
//Function to limit length of Textarea  
function textAreaKeyPress() {
	//alert(obj.name);
    var	obj = document.activeElement;
	var maxLength = obj.getAttribute('maxlength');
	var currentLength = obj.value.length;
	if (currentLength == maxLength){
		event.keyCode = 0
		return false;
	}
}

function textAreaKeyUp() {
	//alert(obj.name);
    var	obj = document.activeElement;
	var maxLength = obj.getAttribute('maxlength');
	var currentLength = obj.value.length;
	if (currentLength > maxLength){
		obj.value = obj.value.substring(0, maxLength);
	}
}
//-------------------------------------------------
//
//check changed when click close Or Waiting 
function checkChangedBeforeClose()
{
    //Start, Duc modified: 20091209
	var change = document.getElementById("ctl00_ContentPlaceHolder1_hdValueChanged");
	//End, Duc modified: 20091209
	//var change = document.getElementById("hdValueChanged");
	if (change)
	{
	    if (change.value == 'true')
	    {
		    var ret = confirm(C_MSG_Changed);
		    if (!ret)
		    {
		       event.returnValue =false;
		       return false; 
		    }
		    else
		    {
		        change.value = 'false';
		    }
        }
    }
    else
    {
        //Start, Test
        //alert("Khong tim thay hdValueChanged");
        //End, Test
    }	
	return true;
}
//END ADD LONGTRAN

//ADD 20080820 YF Check Required for ChangeTaskTitle start
function checkTaskTitleRequied(){
    var TaskTitleControl = document.getElementById('<%=txtTitle %>');
    if (TaskTitleControl){
        if (Trim(TaskTitleControl.value) == ""){
            TaskTitleControl.value = "";
            alert(C_MESS_InputRequied);
            TaskTitleControl.focus();
            event.returnValue = false;
            return false;
        }
    }
    return true;
}

function timeoutDisableSubmitButton()
{
    window.setTimeout("DisableSubmitButton()", 0);
    return true;
}
function DisableSubmitButton()
{
    if(document.getElementById("WFS_btnHTMLApprove")){
        document.getElementById("WFS_btnHTMLApprove").disabled=true;
    }
    if(document.getElementById("WFS_btnHTMLUnApp")){
        document.getElementById("WFS_btnHTMLUnApp").disabled=true;
    }
    if(document.getElementById("WFS_btnHTMLFinish")){
        document.getElementById("WFS_btnHTMLFinish").disabled=true;
    }
    if(document.getElementById("WFS_btnHTMLWaiting")){
        document.getElementById("WFS_btnHTMLWaiting").disabled=true;
    }
    if(document.getElementById("WFS_btnHTMLSave")){
        document.getElementById("WFS_btnHTMLSave").disabled=true;
    }
    if(document.getElementById("WFS_btnHTMLClose")){
        document.getElementById("WFS_btnHTMLClose").disabled=true;
    }
}