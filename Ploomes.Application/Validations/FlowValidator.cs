﻿namespace Ploomes.Application.Validations
{
    /// <summary>Representa um validador de um fluxo de condições sobre um objeto e suas propriedades.</summary>
    public class FlowValidator<T>
    {
        private readonly List<string> _errors = new();
        private bool _hasErrors = false;
        public bool _hasNot = false;
        private Action? _validateAction;
        protected readonly T obj;

        public IEnumerable<string> Errors => _errors;
        public bool IsValid => !_errors.Any();

        protected FlowValidator(T obj)
        {
            this.obj = obj;
        }

        /// <summary>Executa o fluxo de validação e verifica se todas as condições foram válidas.</summary>        
        public bool Validate()
        {
            _validateAction?.Invoke();

            return IsValid;
        }

        /// <summary>Adiciona uma linha de erro no fluxo de validação.</summary>
        public FlowValidator<T> AddError(string error)
        {
            _validateAction += () =>
            {
                if (_hasErrors)
                    _errors.Add(error);

                _hasErrors = false;
            };

            return this;
        }

        /// <summary>Verifica se o objeto corrente é nulo.</summary>
        public FlowValidator<T> IsNull()
        {
            _validateAction += () =>
            {
                _hasErrors = obj == null;
            };

            return this;
        }

        /// <summary>Verifica se uma string é nula, vazia ou espaço em branco.</summary>
        public FlowValidator<T> IsNull(string? value)
        {
            _validateAction += () =>
            {
                _hasErrors = string.IsNullOrWhiteSpace(value);
            };

            return this;
        }

        /// <summary>Nega a última verificação invocada.</summary>
        public FlowValidator<T> Negate()
        {
            _hasErrors = !_hasErrors;
            return this;
        }

        /// <summary>Verifica se o primeiro parâmetro é igual ao segundo.</summary>
        public FlowValidator<T> IsEquals<T1, T2>(T1 value, T2 other)
            where T1 : IEquatable<T1>
            where T2 : IEquatable<T2>
        {
            _validateAction += () =>
            {
                _hasErrors = value.Equals(value);
            };

            return this;
        }

        /// <summary>Verifica se um inteiro é menor que zero.</summary>
        public FlowValidator<T> IsNegative(long value)
        {
            _validateAction += () =>
            {
                _hasErrors = value < 0;
            };

            return this;
        }

        /// <summary>Verifica se um inteiro é menor que zero.</summary>
        public FlowValidator<T> IsZeroOrNegative(long value)
        {
            _validateAction += () =>
            {
                _hasErrors = value < 0;
            };

            return this;
        }

        /// <summary>Verifica se um inteiro é menor que zero.</summary>
        public FlowValidator<T> IsZeroOrNegative(decimal value)
        {
            _validateAction += () =>
            {
                _hasErrors = value < 0;
            };

            return this;
        }

        /// <summary>Verifica se número é menor que zero.</summary>
        public FlowValidator<T> IsNegative(double value)
        {
            _validateAction += () =>
            {
                _hasErrors = value <= 0.0;
            };

            return this;
        }

        /// <summary>Verifica se número é menor que zero.</summary>
        public FlowValidator<T> IsZeroOrNegative(double value)
        {
            _validateAction += () =>
            {
                _hasErrors = value <= 0.0;
            };

            return this;
        }

        /// <summary>Verifica se número é maior ou igual a zero.</summary>
        public FlowValidator<T> IsPositive(int value)
        {
            _validateAction += () =>
            {
                _hasErrors = value >= 0;
            };

            return this;
        }

        /// <summary>Verifica uma condição definida pelo usuário.</summary>
        public FlowValidator<T> Condition<TValue>(TValue value, Func<TValue, bool> condition)
        {
            _validateAction += () =>
            {
                _hasErrors = condition.Invoke(value);
            };

            return this;
        }
    }
}
