namespace Ploomes.Application.Errors
{
    public class AppError
    {
        public static ErrorValue NoDataHasBeenReported
                => new("app_000", "Nenhum dado foi informado.");

        public static ErrorValue InvalidAccessLevel
                => new("app_001", "Nível de acesso inválido.");

        public class User
        {
            public static ErrorValue NameNotInformed
                => new("user_000", "O nome do usuário deve ser informado.");

            public static ErrorValue EmailNotInformed
                => new("user_001", "O campo email deve ser informado.");

            public static ErrorValue PasswordNotInformed
                => new("user_002", "A senha deve ser informada.");

            public static ErrorValue AlreadyExistingEmail
                => new("user_003", "Já existe uma conta cadastrada com esse email.");

            public static ErrorValue InvalidLogin
                => new("user_004", "Dados de login inválido.");

            public static ErrorValue InvalidPermissionToCreateUser
                => new("user_005", "Somente administradores com privilégios podem criar outros administradores.");

            public static ErrorValue NotFound
                => new("user_007", "O usuário especificado não foi encontrado.");

            public static ErrorValue InvalidPermission
                => new("user_008", "Permissão inválida para prosseguir com a ação.");
        }

        public class Seller
        {            
        }

        public class Product
        {
            public static ErrorValue TitleNotInformed
                => new("prod_000", "O título deve ser informado.");

            public static ErrorValue DescriptionNotInformed
                => new("prod_001", "A descrição deve ser informada.");

            public static ErrorValue DiscountCannotBeNegative
                => new("prod_002", "O desconto não pode ter um valor negativo");

            public static ErrorValue PriceCannotBeZeroOrNegative
                => new("prod_003", "O anúncio precisa ter um preço válido.");

            public static ErrorValue DiscountCannotBeGreaterThen
               => new("prod_004", "O desconto não pode ser maior que 90%");

            public static ErrorValue NotFound
               => new("prod_005", "O produto informado não foi encontrado.");

            public static ErrorValue StockExceeded
               => new("prod_006", "O produto não tem estoque suficiente para a quantidade informada.");
        }
    }
}
