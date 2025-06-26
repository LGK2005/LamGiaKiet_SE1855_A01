using DataAccessLayer;
using DataAccessLayer.Models;

namespace BusinessLogicLayer
{
    // Provides static methods for validating input fields and business rules
    public static class ValidationHelper
    {
        public static string ValidateCustomerProperty(string propertyName, object instance)
        {
            var vm = instance as dynamic;
            switch (propertyName)
            {
                case nameof(Customer.CompanyName):
                    return string.IsNullOrWhiteSpace(vm.CompanyName) ? "Company Name is required." : string.Empty;
                case nameof(Customer.Phone):
                    return string.IsNullOrWhiteSpace(vm.Phone) ? "Phone is required." : string.Empty;
                default:
                    return string.Empty;
            }
        }

        // Model-level validation for Customer
        public static OperationResult ValidateCustomer(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.CompanyName))
                return OperationResult.Fail("Company Name is required.");
            if (string.IsNullOrWhiteSpace(customer.Phone))
                return OperationResult.Fail("Phone is required.");
            return OperationResult.Ok();
        }

        // Property-level validation for Product
        public static string ValidateProductProperty(string propertyName, object instance)
        {
            var vm = instance as dynamic;
            switch (propertyName)
            {
                case nameof(Product.ProductName):
                    return string.IsNullOrWhiteSpace(vm.ProductName) ? "Product Name is required." : string.Empty;
                case nameof(Product.UnitPrice):
                    return vm.UnitPrice == null || vm.UnitPrice < 0 ? "Unit Price must be non-negative." : string.Empty;
                case nameof(Product.UnitsInStock):
                    return vm.UnitsInStock == null || vm.UnitsInStock < 0 ? "Units In Stock must be non-negative." : string.Empty;
                default:
                    return string.Empty;
            }
        }

        // Model-level validation for Product
        public static OperationResult ValidateProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.ProductName))
                return OperationResult.Fail("Product Name is required.");
            if (product.UnitPrice == null || product.UnitPrice < 0)
                return OperationResult.Fail("Unit Price must be non-negative.");
            if (product.UnitsInStock == null || product.UnitsInStock < 0)
                return OperationResult.Fail("Units In Stock must be non-negative.");
            return OperationResult.Ok();
        }

        // Property-level validation for Order
        public static string ValidateOrderProperty(string propertyName, object instance)
        {
            var vm = instance as dynamic;
            switch (propertyName)
            {
                case nameof(Order.CustomerID):
                    return vm.CustomerID <= 0 ? "Customer ID is required." : string.Empty;
                case nameof(Order.EmployeeID):
                    return vm.EmployeeID <= 0 ? "Employee ID is required." : string.Empty;
                default:
                    return string.Empty;
            }
        }

        // Model-level validation for Order
        public static OperationResult ValidateOrder(Order order)
        {
            if (order.CustomerID <= 0)
                return OperationResult.Fail("Customer ID is required.");
            if (order.EmployeeID <= 0)
                return OperationResult.Fail("Employee ID is required.");
            return OperationResult.Ok();
        }
    }
} 