using CNWeb.Helper;
using Models.Dao;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CNWeb.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // [GET] Admin/Order/Order
        [HasCredential(RoleID = "VIEW_ADMIN")]
        public ActionResult Order()
        {
            return PartialView("Order");
        }

        // [GET] Admin/Order/GetPaggedData
        public ActionResult GetPaggedData(string filter, string search, int pageNumber, int pageSize)
        {
            var dao = new OrderModel();
            var model = dao.ListOrder(Int32.Parse(filter), search).ToList();
            var pagedData = Pagination.PagedResult(model, pageNumber, pageSize);
            return Json(pagedData, JsonRequestBehavior.AllowGet);
        }

        // [GET] Admin/Order/UpdateOrder
        public JsonResult UpdateOrder(string id, string status)
        {
            try
            {
                var model = new OrderModel();
                var res = model.ChangeStatusOrder(Int32.Parse(id), Int32.Parse(status));
                if (res)
                {
                    return Json(new { message = "Success!!", data = "Cập nhật đơn hàng thành công" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Fail!!", data = "Cập nhật đơn hàng thất bại" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch
            {
                return Json(new { message = "Fail!!", data = "Cập nhật đơn hàng thất bại" }, JsonRequestBehavior.AllowGet);
            }
        }

        // [GET] Admin/Order/DeleteOrder
        public JsonResult DeleteOrder(string id)
        {
            try
            {
                var model = new OrderModel();
                var res = model.DeleteOrder(Int32.Parse(id));
                if (res == 1)
                {
                    return Json(new { message = "Success!!", data = "Xóa đơn hàng thành công" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Fail!!", data = "Xóa đơn hàng thất bại" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch
            {
                return Json(new { message = "Fail!!", data = "Xóa đơn hàng thất bại" }, JsonRequestBehavior.AllowGet);
            }
        }

        // [GET] Admin/Order/DetailOrder
        public JsonResult DetailOrder(string id)
        {
            var dao = new OrderModel();
            var detail = dao.GetDetailOrderByID(Int32.Parse(id));
            return Json(detail, JsonRequestBehavior.AllowGet);
        }

        // export Excel
        public void ExportExcel_EPPlus()
        {
            var dao = new OrderModel();
            var model = dao.Order().ToList();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1"].Value = "Mã đơn hàng";
            Sheet.Cells["B1"].Value = "Ngày bán";
            Sheet.Cells["C1"].Value = "Tên Khách hàng";
            Sheet.Cells["D1"].Value = "Địa chỉ giao";
            Sheet.Cells["E1"].Value = "Phương thức thanh toán";
            Sheet.Cells["F1"].Value = "Trạng thái đơn hàng";
            int row = 2;
            foreach(var item in model)
            {
                Sheet.Cells[row, 1].Value = item.ID;
                Sheet.Cells[row, 2].Value = item.CreatedTime;
                Sheet.Cells[row, 3].Value = item.CustomerName;
                Sheet.Cells[row, 4].Value = item.CustomerAddress;
                Sheet.Cells[row, 5].Value = item.PaymentMethod;
                Sheet.Cells[row, 6].Value = convertStatus(item.Status);
                row++;
            }

            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Report.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }
        
        // function support
        public string convertStatus(int? status)
        {
            string string_status = "";
            switch (status)
            {
                case 0:
                    string_status = "Đơn hàng đã đặt";
                    break;
                case 1:
                    string_status = "Đã thanh toán";
                    break;
                case 2:
                    string_status = "Đã giao cho ĐVVC";
                    break;
                case 3:
                    string_status = "Đơn hàng Đã nhận";
                    break;
                case 4:
                    string_status = "Đơn hàng Đã giao";
                    break;
                case 5:
                    string_status = "Đơn hàng đã hủy";
                    break;
            }
            return string_status;
        }
    }
}