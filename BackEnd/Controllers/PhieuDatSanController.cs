﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MFFMS.API.Data.PhieuDatSanRepository;
using MFFMS.API.Dtos.PhieuDatSanDto;
using MFFMS.API.Dtos.ResponseDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MFFMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PhieuDatSanController : ControllerBase
    {
        private readonly IPhieuDatSanRepository _repo;
        private readonly IMapper _mapper;
        private readonly string _entityName;
        public PhieuDatSanController(IPhieuDatSanRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _entityName = "phiếu đặt sân";
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PhieuDatSanParams userParams)
        {

            try
            {
                var result = await _repo.GetAll(userParams);
                var resultToReturn = _mapper.Map<IEnumerable<PhieuDatSanForListDto>>(result);

                Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Lấy danh sách tất cả các " + _entityName + " thành công!",
                    Result = new SuccessResponseResultWithMultipleDataDto
                    {
                        Data = resultToReturn,
                        TotalItems = _repo.GetTotalItems(),
                        TotalPages = _repo.GetTotalPages(),
                        PageNumber = userParams.PageNumber,
                        PageSize = userParams.PageSize,
                        StatusStatistics = _repo.GetStatusStatistics(userParams)
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Lấy danh sách tất cả các " + _entityName + " thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetGeneralStatistics([FromQuery] PhieuDatSanGeneralStatisticsParams userParams)
        {
            try
            {
                var result = await _repo.GetGeneralStatistics(userParams);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Lấy dữ liệu thống kê tổng quan về " + _entityName + " thành công!",
                    Result = new SuccessResponseResultWithSingleDataDto
                    {
                        Data = result
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Lấy dữ liệu thống kê tổng quan về " + _entityName + " thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await _repo.GetById(id);
                var resultToReturn = _mapper.Map<PhieuDatSanForViewDto>(result);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Lấy thông tin chi tiết của " + _entityName + " thành công!",
                    Result = new SuccessResponseResultWithSingleDataDto
                    {
                        Data = resultToReturn
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Lấy thông tin chi tiết của " + _entityName + " thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(PhieuDatSanForCreateDto phieuDatSan)
        {
            try
            {
                var result = await _repo.Create(phieuDatSan);

                return StatusCode(201, new SuccessResponseDto
                {
                    Message = "Tạo " + _entityName + " mới thành công!",
                    Result = new SuccessResponseResultWithSingleDataDto
                    {
                        Data = result
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Tạo " + _entityName + " mới thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(string id, PhieuDatSanForUpdateDto phieuDatSan)
        {
            try
            {
                var result = await _repo.UpdateById(id, phieuDatSan);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Cập nhật " + _entityName + " thành công!",
                    Result = new SuccessResponseResultWithSingleDataDto
                    {
                        Data = result
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Cập nhật " + _entityName + " thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> PermanentlyDeleteById(string id)
        {
            try
            {
                var result = await _repo.PermanentlyDeleteById(id);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Xóa " + _entityName + " thành công!",
                    Result = new SuccessResponseResultWithSingleDataDto
                    {
                        Data = result
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Xóa " + _entityName + " thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }
    }
}