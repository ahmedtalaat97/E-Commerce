using AdminDashboard.Helpers;
using AdminDashboard.Models;
using AutoMapper;
using E_Commerce_Core.DataTransferObjects;
using E_Commerce_Core.Enities;
using E_Commerce_Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace AdminDashboard.Controllers
{
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products= await _unitOfWork.Repository<E_Commerce_Core.Enities.Product, int>().GetAllAsync();
            var mappedProducts= _mapper.Map<IEnumerable<ProductToReturnDto>>(products);
            return View(mappedProducts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.Image!=null)
                {
                    model.PictureUrl = DocumentSettings.UploadFile(model.Image, "products");

                }
                var mappedProduct=_mapper.Map<E_Commerce_Core.Enities.Product>(model);

                await _unitOfWork.Repository<E_Commerce_Core.Enities.Product, int>().AddAsync(mappedProduct);
                await _unitOfWork.CompleteAsync();

                return RedirectToAction("Index");

            }

            return View(model);
        }

    }
}
