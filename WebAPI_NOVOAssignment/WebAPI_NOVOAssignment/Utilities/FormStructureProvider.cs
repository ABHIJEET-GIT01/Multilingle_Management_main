using WebAPI_NOVOAssignment.DTOs;
using System.Text.Json;

namespace WebAPI_NOVOAssignment.Utilities;

/// <summary>
/// Utility class for generating localized form structures from JSON files
/// </summary>
public static class FormStructureProvider
{
    private static readonly Dictionary<string, Dictionary<string, FormStructureDto>> LocalizedForms = new();

    static FormStructureProvider()
    {
        LoadFormsFromJsonFiles();
    }

    private static void LoadFormsFromJsonFiles()
    {
        var resourcePath = Path.Combine(AppContext.BaseDirectory, "Resources", "Localization");

        var cultures = new[] { "en", "hi", "mr" };

        foreach (var culture in cultures)
        {
            var filePath = Path.Combine(resourcePath, $"forms.{culture}.json");

            if (File.Exists(filePath))
            {
                try
                {
                    var json = File.ReadAllText(filePath);
                    var forms = JsonSerializer.Deserialize<Dictionary<string, FormStructureDto>>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (forms != null)
                    {
                        LocalizedForms[culture] = forms;
                    }
                    else
                    {
                        LocalizedForms[culture] = GetDefaultFormsForCulture(culture);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading forms for culture {culture}: {ex.Message}");
                    LocalizedForms[culture] = GetDefaultFormsForCulture(culture);
                }
            }
            else
            {
                // If file doesn't exist, use default forms for that culture
                LocalizedForms[culture] = GetDefaultFormsForCulture(culture);
            }
        }

        // Ensure all cultures are populated
        if (!LocalizedForms.ContainsKey("en"))
        {
            LocalizedForms["en"] = GetDefaultFormsForCulture("en");
        }
        if (!LocalizedForms.ContainsKey("hi"))
        {
            LocalizedForms["hi"] = GetDefaultFormsForCulture("hi");
        }
        if (!LocalizedForms.ContainsKey("mr"))
        {
            LocalizedForms["mr"] = GetDefaultFormsForCulture("mr");
        }
    }

    private static Dictionary<string, FormStructureDto> GetDefaultFormsForCulture(string culture)
    {
        return culture.ToLowerInvariant() switch
        {
            "hi" => GetDefaultHindiForms(),
            "mr" => GetDefaultMarathiForms(),
            _ => GetDefaultEnglishForms()
        };
    }

    private static Dictionary<string, FormStructureDto> GetDefaultEnglishForms()
    {
        return new Dictionary<string, FormStructureDto>
        {
            {
                "login", new FormStructureDto
                {
                    Title = "Login",
                    Fields = new List<FormFieldDto>
                    {
                        new FormFieldDto
                        {
                            Name = "email",
                            Type = "email",
                            Label = "Email Address",
                            Placeholder = "Enter your email",
                            Required = true
                        },
                        new FormFieldDto
                        {
                            Name = "password",
                            Type = "password",
                            Label = "Password",
                            Placeholder = "Enter your password",
                            Required = true
                        }
                    }
                }
            },
            {
                "register", new FormStructureDto
                {
                    Title = "Register",
                    Fields = new List<FormFieldDto>
                    {
                        new FormFieldDto
                        {
                            Name = "username",
                            Type = "text",
                            Label = "Username",
                            Placeholder = "Choose a username",
                            Required = true
                        },
                        new FormFieldDto
                        {
                            Name = "email",
                            Type = "email",
                            Label = "Email Address",
                            Placeholder = "Enter your email",
                            Required = true
                        },
                        new FormFieldDto
                        {
                            Name = "password",
                            Type = "password",
                            Label = "Password",
                            Placeholder = "Create a strong password",
                            Required = true
                        },
                        new FormFieldDto
                        {
                            Name = "firstName",
                            Type = "text",
                            Label = "First Name",
                            Placeholder = "Enter your first name",
                            Required = false
                        },
                        new FormFieldDto
                        {
                            Name = "lastName",
                            Type = "text",
                            Label = "Last Name",
                            Placeholder = "Enter your last name",
                            Required = false
                        }
                    }
                }
            }
        };
    }

    private static Dictionary<string, FormStructureDto> GetDefaultHindiForms()
    {
        return new Dictionary<string, FormStructureDto>
        {
            {
                "login", new FormStructureDto
                {
                    Title = "लॉगिन करें",
                    Fields = new List<FormFieldDto>
                    {
                        new FormFieldDto
                        {
                            Name = "email",
                            Type = "email",
                            Label = "ईमेल पता",
                            Placeholder = "अपना ईमेल दर्ज करें",
                            Required = true
                        },
                        new FormFieldDto
                        {
                            Name = "password",
                            Type = "password",
                            Label = "पासवर्ड",
                            Placeholder = "अपना पासवर्ड दर्ज करें",
                            Required = true
                        }
                    }
                }
            },
            {
                "register", new FormStructureDto
                {
                    Title = "पंजीकरण करें",
                    Fields = new List<FormFieldDto>
                    {
                        new FormFieldDto
                        {
                            Name = "username",
                            Type = "text",
                            Label = "उपयोगकर्ता नाम",
                            Placeholder = "एक उपयोगकर्ता नाम चुनें",
                            Required = true
                        },
                        new FormFieldDto
                        {
                            Name = "email",
                            Type = "email",
                            Label = "ईमेल पता",
                            Placeholder = "अपना ईमेल दर्ज करें",
                            Required = true
                        },
                        new FormFieldDto
                        {
                            Name = "password",
                            Type = "password",
                            Label = "पासवर्ड",
                            Placeholder = "एक मजबूत पासवर्ड बनाएँ",
                            Required = true
                        },
                        new FormFieldDto
                        {
                            Name = "firstName",
                            Type = "text",
                            Label = "पहला नाम",
                            Placeholder = "अपना पहला नाम दर्ज करें",
                            Required = false
                        },
                        new FormFieldDto
                        {
                            Name = "lastName",
                            Type = "text",
                            Label = "अंतिम नाम",
                            Placeholder = "अपना अंतिम नाम दर्ज करें",
                            Required = false
                        }
                    }
                }
            }
        };
    }

    private static Dictionary<string, FormStructureDto> GetDefaultMarathiForms()
    {
        return new Dictionary<string, FormStructureDto>
        {
            {
                "login", new FormStructureDto
                {
                    Title = "लॉगिन करा",
                    Fields = new List<FormFieldDto>
                    {
                        new FormFieldDto
                        {
                            Name = "email",
                            Type = "email",
                            Label = "ईमेल पत्ता",
                            Placeholder = "आपली ईमेल प्रविष्ट करा",
                            Required = true
                        },
                        new FormFieldDto
                        {
                            Name = "password",
                            Type = "password",
                            Label = "पासवर्ड",
                            Placeholder = "आपला पासवर्ड प्रविष्ट करा",
                            Required = true
                        }
                    }
                }
            },
            {
                "register", new FormStructureDto
                {
                    Title = "नोंदणी करा",
                    Fields = new List<FormFieldDto>
                    {
                        new FormFieldDto
                        {
                            Name = "username",
                            Type = "text",
                            Label = "वापरकर्ता नाव",
                            Placeholder = "एक वापरकर्ता नाव निवडा",
                            Required = true
                        },
                        new FormFieldDto
                        {
                            Name = "email",
                            Type = "email",
                            Label = "ईमेल पत्ता",
                            Placeholder = "आपली ईमेल प्रविष्ट करा",
                            Required = true
                        },
                        new FormFieldDto
                        {
                            Name = "password",
                            Type = "password",
                            Label = "पासवर्ड",
                            Placeholder = "एक मजबूत पासवर्ड तयार करा",
                            Required = true
                        },
                        new FormFieldDto
                        {
                            Name = "firstName",
                            Type = "text",
                            Label = "पहिले नाव",
                            Placeholder = "आपले पहिले नाव प्रविष्ट करा",
                            Required = false
                        },
                        new FormFieldDto
                        {
                            Name = "lastName",
                            Type = "text",
                            Label = "शेवटचे नाव",
                            Placeholder = "आपले शेवटचे नाव प्रविष्ट करा",
                            Required = false
                        }
                    }
                }
            }
        };
    }

    public static FormStructureDto? GetFormStructure(string formType, string culture = "en")
    {
        var cultureLower = culture.ToLowerInvariant();

        // Ensure the requested culture is in the dictionary
        if (!LocalizedForms.ContainsKey(cultureLower))
        {
            LocalizedForms[cultureLower] = GetDefaultFormsForCulture(cultureLower);
        }

        // Get forms for the requested culture
        if (LocalizedForms.TryGetValue(cultureLower, out var cultureForms))
        {
            if (cultureForms.TryGetValue(formType.ToLowerInvariant(), out var form))
                return form;
        }

        // Fallback to English if specific culture or form not found
        if (cultureLower != "en" && LocalizedForms.TryGetValue("en", out var englishForms))
        {
            if (englishForms.TryGetValue(formType.ToLowerInvariant(), out var englishForm))
                return englishForm;
        }

        return null;
    }
}
