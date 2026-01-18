namespace WebAPI_NOVOAssignment.Services;

/// <summary>
/// Service for managing application localization and messages
/// </summary>
public interface ILocalizationService
{
    string GetMessage(string key, string culture = "en");
    IEnumerable<string> GetAvailableCultures();
}

/// <summary>
/// Implementation of localization service using JSON files
/// </summary>
public class LocalizationService : ILocalizationService
{
    private readonly Dictionary<string, Dictionary<string, string>> _messages;

    public LocalizationService()
    {
        _messages = new Dictionary<string, Dictionary<string, string>>
        {
            {
                "en", new Dictionary<string, string>
                {
                    { "login_success", "Login successful" },
                    { "login_failed", "Invalid email or password" },
                    { "registration_success", "Registration successful" },
                    { "registration_failed", "Failed to register - invalid data or email/username already exists" },
                    { "user_created", "User created successfully" },
                    { "user_updated", "User updated successfully" },
                    { "user_deleted", "User deleted successfully" },
                    { "user_not_found", "User not found" },
                    { "users_loaded", "Users loaded successfully" },
                    { "validation_failed", "Validation failed" },
                    { "invalid_request", "Invalid request" },
                    { "token_refreshed", "Token refreshed successfully" },
                    { "token_revoked", "Token revoked successfully" },
                    { "revoke_failed", "Failed to revoke token" },
                    { "invalid_token", "Invalid or expired refresh token" },
                    { "form_not_found", "Form not found" },
                    { "role_created", "Role created successfully" },
                    { "role_updated", "Role updated successfully" },
                    { "role_deleted", "Role deleted successfully" },
                    { "role_not_found", "Role not found" },
                    { "roles_loaded", "Roles loaded successfully" }
                }
            },
            {
                "hi", new Dictionary<string, string>
                {
                    { "login_success", "लॉगिन सफल" },
                    { "login_failed", "अमान्य ईमेल या पासवर्ड" },
                    { "registration_success", "पंजीकरण सफल" },
                    { "registration_failed", "पंजीकरण विफल - अमान्य डेटा या ईमेल/उपयोगकर्ता नाम पहले से मौजूद है" },
                    { "user_created", "उपयोगकर्ता सफलतापूर्वक बनाया गया" },
                    { "user_updated", "उपयोगकर्ता सफलतापूर्वक अपडेट किया गया" },
                    { "user_deleted", "उपयोगकर्ता सफलतापूर्वक हटाया गया" },
                    { "user_not_found", "उपयोगकर्ता नहीं मिला" },
                    { "users_loaded", "उपयोगकर्ता सफलतापूर्वक लोड किए गए" },
                    { "validation_failed", "सत्यापन विफल" },
                    { "invalid_request", "अमान्य अनुरोध" },
                    { "token_refreshed", "टोकन सफलतापूर्वक रीफ्रेश किया गया" },
                    { "token_revoked", "टोकन सफलतापूर्वक रद्द किया गया" },
                    { "revoke_failed", "टोकन रद्द करने में विफल" },
                    { "invalid_token", "अमान्य या समाप्त टोकन" },
                    { "form_not_found", "फॉर्म नहीं मिला" },
                    { "role_created", "भूमिका सफलतापूर्वक बनाई गई" },
                    { "role_updated", "भूमिका सफलतापूर्वक अपडेट की गई" },
                    { "role_deleted", "भूमिका सफलतापूर्वक हटाई गई" },
                    { "role_not_found", "भूमिका नहीं मिली" },
                    { "roles_loaded", "भूमिकाएं सफलतापूर्वक लोड की गईं" }
                }
            },
            {
                "mr", new Dictionary<string, string>
                {
                    { "login_success", "लॉगिन यशस्वी" },
                    { "login_failed", "अमान्य ईमेल किंवा पासवर्ड" },
                    { "registration_success", "नोंदणी यशस्वी" },
                    { "registration_failed", "नोंदणी अयशस्वी - अमान्य डेटा किंवा ईमेल/वापरकर्ता नाव आधीपासून अस्तित्वात आहे" },
                    { "user_created", "वापरकर्ता यशस्वीरित्या तयार केला गेला" },
                    { "user_updated", "वापरकर्ता यशस्वीरित्या अद्यतन केला गेला" },
                    { "user_deleted", "वापरकर्ता यशस्वीरित्या हटवला गेला" },
                    { "user_not_found", "वापरकर्ता सापडला नाही" },
                    { "users_loaded", "वापरकर्ता यशस्वीरित्या लोड केले गेले" },
                    { "validation_failed", "प्रमाणीकरण अयशस्वी" },
                    { "invalid_request", "अमान्य विनंती" },
                    { "token_refreshed", "टोकन यशस्वीरित्या रीफ्रेश केला गेला" },
                    { "token_revoked", "टोकन यशस्वीरित्या रद्द केला गेला" },
                    { "revoke_failed", "टोकन रद्द करण्यात अयशस्वी" },
                    { "invalid_token", "अमान्य किंवा समाप्त टोकन" },
                    { "form_not_found", "फॉर्म सापडला नाही" },
                    { "role_created", "भूमिका यशस्वीरित्या तयार केली गेली" },
                    { "role_updated", "भूमिका यशस्वीरित्या अद्यतन केली गेली" },
                    { "role_deleted", "भूमिका यशस्वीरित्या हटवली गेली" },
                    { "role_not_found", "भूमिका सापडली नाही" },
                    { "roles_loaded", "भूमिका यशस्वीरित्या लोड केल्या गेल्या" }
                }
            }
        };
    }
   
    public string GetMessage(string key, string culture = "en")
    {
        var cultureLower = culture.ToLowerInvariant();

        if (!_messages.ContainsKey(cultureLower))
            cultureLower = "en";

        if (_messages[cultureLower].TryGetValue(key.ToLowerInvariant(), out var message))
            return message;

        return key;
    }
   
    public IEnumerable<string> GetAvailableCultures()
    {
        return _messages.Keys;
    }
}
