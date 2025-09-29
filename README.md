# RoadFire

En la consola de visual estudio seleccionar el proyecto como lo muesta la imagen.  
<img width="899" height="303" alt="image" src="https://github.com/user-attachments/assets/782be2d1-7299-4ecf-b28d-f6d498ff3989" />  

Para ejecutar la base de datos se deben ejecutar las siguientes lineas  
Add-Migration InitialDB -Project MS.RoadFire.DataAccess -StartupProject MS.RoadFire.Api  
Update-Database -Project MS.RoadFire.DataAccess -StartupProject MS.RoadFire.Api 
