using Application.DTOs;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductsController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        var products = await _productService.GetAllProductsAsync();
        var dtos = _mapper.Map<IEnumerable<ProductDto>>(products);
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) return NotFound($"Product with ID {id} not found.");
        return Ok(_mapper.Map<ProductDto>(product));
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create(CreateProductDto dto)
    {
        var productEntity = _mapper.Map<Product>(dto);
        var created = await _productService.CreateProductAsync(productEntity);
        var productDto = _mapper.Map<ProductDto>(created);
        return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, productDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductDto>> Update(int id, UpdateProductDto dto)
    {
        // Map dto to entity and set only the fields allowed to update
        var updatedEntity = _mapper.Map<Product>(dto);
        var updated = await _productService.UpdateProductAsync(id, updatedEntity);
        if (updated == null) return NotFound($"Product with ID {id} not found.");
        return Ok(_mapper.Map<ProductDto>(updated));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _productService.DeleteProductAsync(id);
        if (!success) return NotFound($"Product with ID {id} not found.");
        return NoContent();
    }
}
