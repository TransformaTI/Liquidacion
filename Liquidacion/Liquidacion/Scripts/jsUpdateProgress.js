//Código JavaScript incluido en un archivo denominado jsUpdateProgress.js 
Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq); 
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);  
  function beginReq(sender, args)
{     // muestra el popup  
   $find(ModalProgress).show();

}
  function endReq(sender, args) 
{     //  esconde el popup     
$find(ModalProgress).hide(); 
}

