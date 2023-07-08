using ErrorOr;

namespace CareerLinkServer.ServiceErrors;

public static class DomainErrors
{
    public static class Products
    {
        /*
         * Note that NotFound is an instance of the Error class .
         */
        public static Error NotFound => Error.NotFound(
            code: "Products.NotFound",
            description: "Product with the  provided Id was not found"
            );

    }
    
}