using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using PerfectMatchBack.DTOs;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;
using PerfectMatchBack.Services.Implementation;
using PerfectMatchBack.Utilitles;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<PetFectMatchContext>(option => option.UseSqlServer
(builder.Configuration.GetConnectionString("Connection")));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAccessService, AccessService>();
builder.Services.AddScoped<IAnimalTypeService,AnimalTypeService>();
builder.Services.AddScoped<IBreedService, BreedService>();
builder.Services.AddScoped<IGenderService, GenderService>();
builder.Services.AddScoped<ICityService, CityService>();    
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IRoleService, RoleService>();    
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
var key = builder.Configuration.GetValue<string>("JwtSettings:key");
var keyBites = Encoding.ASCII.GetBytes(key);

builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();

    });
}
);
builder.Services.AddAuthentication(confg =>
{
    confg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    confg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBites),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
/*
#region Peticiones API REST
#region Access
app.MapGet("Access/List",async (
    IMapper _mapper,
    IAccessService _service
    ) => {
        var acc = await _service.listAccess();
        var listDTO = _mapper.Map<List<AccessDTO>>(acc);
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
app.MapGet("Breed/List", async (
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
    IImageService _service
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
    IImageService service,
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

app.MapDelete("Image/Delete/{idImage}", async (
    int idImage,
    IImageService _service
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
app.MapGet("Publication/userList/{idUser}",async (
    int idUser,
    IMapper _mapper,
    IPostService _service
    ) => {
    var list = await _service.userPublications(idUser);
        var listDTO = _mapper.Map<List<PublicationDTO>>(list);
        if (listDTO is not null)
        {
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
    IImageService _serviceImage,
    IMapper _mapper
    ) =>
{
    var modelTrue = await _service.GetPublication(idPublication);
    if (modelTrue is null) return Results.NotFound();
    var publication = _mapper.Map<Publication>(model);
    modelTrue.Age = publication.Age;
    modelTrue.IdOwner = publication.IdOwner;
    modelTrue.Description = publication.Description;
    modelTrue.Comments = publication.Comments;
    modelTrue.AnimalName = publication.AnimalName;
    modelTrue.IdBreed = publication.IdBreed;
    modelTrue.IdAnimalType = publication.IdAnimalType;
    modelTrue.Price = publication.Price;
    modelTrue.IdGender = publication.IdGender;
    modelTrue.Weight = publication.Weight;
    if (publication.Images.Count > 0)
    {
        foreach (var im in publication.Images)
        {
            var images = await _serviceImage.GetImage(im.IdImage);
            if (images is not null)
            {
                images.DataImage = im.DataImage;
                await _serviceImage.Updatemgae(images);
            }

        }
    }
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
    IImageService _imageService

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
        userTrue.CodePay = model.CodePay;
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
*/
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("NuevaPolitica");
app.UseHttpsRedirection();




app.MapControllers();

app.Run();

