using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.DTOs;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Services.Implementation
{
    public class PostService : IPostService
    {
        private PetFectMatchContext _context;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        public PostService(PetFectMatchContext context, IMapper mapper, IImageService imageService)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
        }
        public async Task<Publication> AddPublication(Publication model)
        {
            try
            {
                _context.Publications.Add(model);
                await _context.SaveChangesAsync();
                return model;

            } catch (Exception ex) {
                throw ex;
            }
        }

     

        public async Task<bool> DeletePublication(Publication model)
        {
            try
            {
                var ima = await _context.Publications.Include(id => id.Images).FirstOrDefaultAsync(id => id.IdPublication == model.IdPublication);
                _context.Images.RemoveRange(ima.Images);

                _context.Publications.Remove(model);
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<Publication> GetPublication(int id)
        {
            try
            {
                var publication = await _context.Publications.Include(navi => navi.IdOwnerNavigation).Include(navi => navi.IdCityNavigation).Include(navi => navi.IdAnimalTypeNavigation)
                    .Include(navi => navi.IdBreedNavigation).Include(navi => navi.Images)
                    .Where(model => model.IdPublication == id).FirstOrDefaultAsync()
                    ;
                return publication;

            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<Image>> ListImage(int id)
        {
            try
            {
                var images = await _context.Images.Where(ide => ide.IdPublicationNavigation.IdPublication == id).ToListAsync();
                return images;
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Publication>> ListPublication()
        {
            try
            {
                var list = await _context.Publications.Include(navi => navi.IdOwnerNavigation).Include(navi => navi.IdCityNavigation).Include(navi => navi.IdAnimalTypeNavigation).Include(navi => navi.IdBreedNavigation).
                    Include(id => id.IdGenderNavigation).Include(navi => navi.Images).
                    ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Publication> UpdatePublication(PublicationDTO model, int idPublication)
        {
            try
            {
                var modelTrue = await _context.Publications.FindAsync(idPublication);
                if (modelTrue is null) return null;
                var publication = _mapper.Map<Publication>(model);
                modelTrue.Age = publication.Age;
                modelTrue.IdOwner = publication.IdOwner;
                modelTrue.Description = publication.Description;
                modelTrue.Comments = publication.Comments;
                modelTrue.AnimalName = publication.AnimalName;
                modelTrue.IdBreed = publication.IdBreed;
                modelTrue.IdAnimalType = publication.IdAnimalType;
                modelTrue.IdGender = publication.IdGender;
                modelTrue.Weight = publication.Weight;
                var listImgs = await _imageService.GetImageFromPublication(idPublication);
                if (publication.Images != null)
                {

                    foreach (var im in publication.Images)
                    {
                        var images = await _imageService.GetImage(im.IdImage);
                        if (images is not null)
                        {
                            listImgs.Remove(listImgs.Find(x => x.IdImage == im.IdImage));
                            if (images.DataImage != im.DataImage)
                            {
                                images.DataImage = im.DataImage;
                                await _imageService.Updatemgae(images);
                            }
                        }
                        else
                        {
                            images = new()
                            {
                                DataImage = im.DataImage,
                                IdPublication = idPublication
                            };
                            await _imageService.addImage(images);

                        }

                    }
                    if (listImgs.Count > 0)
                    {
                        await _imageService.removeRangeImage(listImgs);
                    }

                }

                _context.Publications.Update(publication);

                await _context.SaveChangesAsync();
                return publication;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Publication>> UserPublications(int idUser)
        {
            try
            {
                var list = await _context.Publications.Include(navi => navi.IdOwnerNavigation).Include(navi => navi.IdCityNavigation).Include(navi => navi.IdAnimalTypeNavigation).Include(navi => navi.IdBreedNavigation).
                    Include(id => id.IdGenderNavigation).Include(navi => navi.Images).Where(id => id.IdOwner == idUser).ToListAsync();
                return list;

            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
