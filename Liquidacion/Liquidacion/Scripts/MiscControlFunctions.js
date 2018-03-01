function toggle(ID)
{  
    var ctrlID = document.getElementById(ID);                         
    if (ctrlID.style.display == 'none')
    {
        ctrlID.style.display = 'block';
    }
    else
    {
        ctrlID.style.display = 'none';
    }        
}