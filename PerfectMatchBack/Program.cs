using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.DTOs;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;
using PerfectMatchBack.Services.Implementation;
using PerfectMatchBack.Utilitles;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PerfectMatchContext>(option => option.UseSqlServer
(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAccessService, AccessService>();
builder.Services.AddScoped<IAnimalTypeService,AnimalTypeService>();
builder.Services.AddScoped<IBreedService, BreedService>();
builder.Services.AddScoped<IGenderService, GenderService>();
builder.Services.AddScoped<ICityService, CityService>();    
builder.Services.AddScoped<IIMageService, ImageService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IRoleService, RoleService>();    
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();

    });
}
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Peticiones API REST
#region AnimalType
app.MapGet("AnimalType/List", async (
    IMapper _mapper,
    IAnimalTypeService _service
    ) =>
{
    var list = await _service.listAnimalType();
    var listDTO = _mapper.Map<List<AnimalTypeDTO>>(list);
    if(listDTO.Count > 0)
    {
        return Results.Ok(listDTO);
    }
    else
    {
        return Results.NotFound();
    }
});
#endregion
#region Breed
app.MapPost("Breed/List", async (
    IMapper _mapper,
    IBreedService _service
    ) =>
{ 
   var list = await _service.listBreed();
   var listDTO = _mapper.Map<List<BreedDTO>>(list);
    if (listDTO.Count > 0)
    {
        return Results.Ok(listDTO);
    }
    else
    {
        return Results.NotFound();
    }
    {

    }
});
#endregion
#region Gender
app.MapGet("Gender/List", async (
    IMapper _mapper,
    IGenderService _service
    ) =>
{
    var list = await _service.listGender();
    var listDTO = _mapper.Map<List<GenderDTO>>(list);
    if (listDTO.Count > 0)
    {
        return Results.Ok(listDTO);
    }
    else
    {
        return Results.NotFound();
    }
    {

    }
});
#endregion
#region City
app.MapGet("City/List", async (
    IMapper _mapper,
    ICityService _service   
    ) =>
{
    var list = await _service.listCity();
    var listDTO = _mapper.Map<List<CityDTO>>(list);
    if (listDTO.Count > 0) {
    return Results.Ok(listDTO);
    }
    else
    {
    return Results.NotFound();
    }
});

#endregion
#region Image
app.MapGet("Image/List", async (
    IMapper _mapper,
    IIMageService _service
) => {
    var list = await _service.listImage();
    var listDTO = _mapper.Map<List<ImageDTO>>(list);
    if (listDTO.Count > 0) { 
    return Results.Ok(listDTO);

    }
    else
    {
    return Results.NotFound();
    }
});
app.MapPost("Image/Add", async (
    ImageDTO model,
    IIMageService service,
    IMapper _mapper
    
    ) => {
    var image = _mapper.Map<Image>(model);  
    var imageAdd = await service.addImage(image);
        if (imageAdd is not null) {
            return Results.Ok(_mapper.Map<ImageDTO>(imageAdd));
        }else
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    });
//las imagenes solo se pueden actualizar para ser cambiadas, no se cambiaran el id de publicación para no hacer mas tedioso el proceso
app.MapPut("Image/Update/{idImage}",async ( 
    int idImage,
    ImageDTO model,
    IMapper _mapper,
    IIMageService _service
    
    ) => {
        var image = await _service.GetImage(idImage);
        if (image is null) return Results.NotFound();
        var mapImage = _mapper.Map<Image>(model);
        image.DataImage = mapImage.DataImage;
        var result = await _service.Updatemgae(image);
        if (result) {
            return Results.Ok(_mapper.Map<ImageDTO>(image));
        }
        else
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    });
app.MapDelete("Image/Delete/{idImage}", async (
    int idImage,
    IIMageService _service
    ) => {
        var search = await _service.GetImage(idImage);
        if (search is null) return Results.NotFound();
        var deleteImage = await _service.removeImage(search);
        if (deleteImage) {
            return Results.Ok();
        }
        else
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    });
#endregion
#region Role
app.MapGet("Role/List", async (
    IMapper _mapper,
    IRoleService _service
    ) => {
    var list = await _service.listRole();
    var listDTO = _mapper.Map<List<RoleDTO>>(list);
        if(listDTO.Count > 0)
        {
            return Results.Ok( listDTO);
        }
        else
        {
            return Results.NotFound();
        }
    });
#endregion
#region Publication
app.MapGet("Publication/listImages/{id}",async (
    int id,
    IPostService _service,
    IMapper _mapper 
    ) => {
        var list = await _service.listImage(id);
        var listDTO = _mapper.Map<List<ImageDTO>>(list);
        if (listDTO is not null) {
            return Results.Ok(listDTO);
        }
        else
        {
            return Results.NotFound();
        }
    
    });
app.MapGet("Publication/List", async (
    IMapper _mapper,
    IPostService _service
    ) => {
        var list = await _service.listPublication();
        var listDTO = _mapper.Map<List<PublicationDTO>>(list);
        if (listDTO.Count > 0)
        {
            return Results.Ok(listDTO);
        }
        else
        {
            return Results.NotFound();
        }
    });
app.MapPost("Publication/Add", async (
    PublicationDTO model,
    IMapper _mapper,
    IPostService _service
    ) =>
{
    var post = _mapper.Map<Publication>(model);
    var postAdd = await _service.addPublication(post);
    if (postAdd.IdPublication != 0)
    {
        return Results.Ok(_mapper.Map<PublicationDTO>(postAdd));
    }
    else
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }

});
app.MapPut("Publication/Update/{idPublication}", async (
    int idPublication,
    PublicationDTO model,
    IPostService _service,
    IMapper _mapper
    ) =>
{
    var modelTrue = await _service.GetPublication(idPublication);
    if (modelTrue is null) return Results.NotFound();
    var publication = _mapper.Map<Publication>(model);
    modelTrue.Age = publication.Age;
    modelTrue.Description = publication.Description;
    modelTrue.Comments = publication.Comments;
    modelTrue.AnimalName = publication.AnimalName;
    modelTrue.IdBreed = publication.IdBreed;
    modelTrue.IdAnimalType = publication.IdAnimalType;
    modelTrue.IdGender = publication.IdGender;
    modelTrue.Weight = publication.Weight;
    var ouput = await _service.updatePublication(modelTrue);
    if (ouput)
    {
        return Results.Ok(_mapper.Map<PublicationDTO>(modelTrue));
    }
    else
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }
});
app.MapDelete("Publication/Delete/{idPublication}", async (
    int idPublication,
    IPostService _service,
    IIMageService _imageService

    ) =>

{  
        var postTrue = await _service.GetPublication(idPublication);
    if (postTrue is null) return Results.NotFound();
  
    var deletePost = await _service.deletePublication(postTrue);
    if (deletePost)
    {
      
        return Results.Ok();
    }
    else
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }
});
#endregion
#region User
app.MapGet("User/List", async (

    IMapper _mapper,
    IUserService _userService
    ) =>
{
    var list = await _userService.listUser();
    var lisDTO = _mapper.Map<List<UserDTO>>(list);
    if (lisDTO.Count > 0)
    {
        return Results.Ok(lisDTO);
    }
    else
    {
        return Results.NotFound();
    }

});
app.MapPost("User/Add",async (
    UserDTO model,
    IUserService userService,
    IMapper _mapper,
    IAccessService _accessService
    ) => {
        Encryption enc = new Encryption();
        Access access = new Access();
        access.Password = enc.Encrypt(model.password);
        var addAcces = await _accessService.createAccess(access);
        if (addAcces is null) return Results.StatusCode(StatusCodes.Status500InternalServerError); 

        var modelDTO = _mapper.Map<User>(model);
        modelDTO.IdAccess = addAcces.IdAccess;
        var addUser = await userService.addUser(modelDTO);
        
        if (addUser is not null)
        {
            return Results.Ok(_mapper.Map<UserDTO>(addUser));
        }
        else
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    
    });
app.MapPut("User/Update/{idUser}",async (
    UserDTO model,
    int idUser,
    IUserService userService,
    IMapper _mapper
    ) => {
    var userTrue = await userService.getUser(idUser);
        if (userTrue is null) return Results.StatusCode(StatusCodes.Status500InternalServerError);
        userTrue.Name = model.Name;
        userTrue.Email = model.Email;
        userTrue.IdCity = model.IdCity;
        userTrue.BirthDate = DateTime.ParseExact(model.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        userTrue.IdRole = model.IdRole; 
     var userUpdate = await userService.updateUser(userTrue);
        if (userUpdate) {
            return Results.Ok(_mapper.Map<UserDTO>(userTrue));
        }
        else
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }

    });
app.MapDelete("User/Delete/{idUser}",async (
    int idUser,
    IUserService _service
    ) =>


{
    var userTrue = await _service.getUser(idUser);
    if (userTrue is null) return Results.NotFound();
    var deleteUser = await _service.deleteUser(userTrue);
    if (deleteUser) {

        return Results.Ok();
    }
    else
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }

});
#endregion
#endregion


app.UseCors("NuevaPolitica");
app.Run();

