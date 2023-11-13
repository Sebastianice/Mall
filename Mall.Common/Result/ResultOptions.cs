using Microsoft.AspNetCore.Mvc;

namespace Mall.Common.Result;

public class ResultOptions
{
    private Func<ResultException, IActionResult> _resultFactory = default!;

    public Func<ResultException, IActionResult> ResultFactory
    {
        get => _resultFactory;
        set => _resultFactory = value ?? throw new ArgumentNullException(nameof(value));
    }
}
