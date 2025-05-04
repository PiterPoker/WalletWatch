using System.Text.RegularExpressions;

namespace AuthWalletWatch.Infrastructure.Models;

public class ApplicationProfile
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime? DateOfBirth { get; private set; }
    public string? PhotoUrl { get; private set; }
    public string? PhoneNumber { get; private set; }


    public virtual ApplicationUser User { get; set; }
    public virtual Guid UserId { get; set; }



    public ApplicationProfile()
    {
        Id = Guid.NewGuid();
    }

    public ApplicationProfile(string firstName, string lastName) : this()
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public bool TryChangeFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > 50)
        {
            return false;
        }

        FirstName = firstName;
        return true;
    }

    public bool TryChangeLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > 50)
        {
            return false;
        }

        LastName = lastName;
        return true;
    }

    public bool TryChangeDateOfBirth(DateTime? dateOfBirth)
    {
        if (dateOfBirth.HasValue && dateOfBirth.Value > DateTime.UtcNow)
        {
            return false;
        }

        DateOfBirth = dateOfBirth;
        return true;
    }

    public bool TryChangePhotoUrl(string? photoUrl)
    {
        if (photoUrl != null && !Uri.IsWellFormedUriString(photoUrl, UriKind.Absolute))
        {
            return false;
        }

        PhotoUrl = photoUrl;
        return true;
    }

    public bool TryChangePhoneNumber(string? phoneNumber)
    {
        if (phoneNumber != null && !Regex.IsMatch(phoneNumber, @"^\+?[1-9]\d{1,14}$"))
        {
            return false;
        }

        PhoneNumber = phoneNumber;
        return true;
    }
}
