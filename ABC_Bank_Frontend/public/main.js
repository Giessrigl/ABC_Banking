async function ReceiveContacts(category)
{
      var contacts = document.getElementById('contacts');
      var searchbar = document.getElementById('searchbar');
      var searchPattern = searchbar.value;

      await GetContactInfo('https://localhost:44373/api/contactinfo' + '?' + new URLSearchParams({
            category: category,
            searchPattern: searchPattern
      }))
      .then(data => {
            if (data.length == 0)
            {
                  console.log("Could not receive contacts.");
                  return;
            }
            
            contacts.innerHTML = "";
            for (let i = 0; i < data.length; i++)
            {
                  contacts.innerHTML += AddReceivedContact(data[i]);
            }
      })
      .catch(err => {
            console.error(err);
      }); 
}

function AddReceivedContact(contact)
{
      var htmlImage = "";
      if (contact.ProfilePicture != null)
      {
            htmlImage = '<img src="' + Base64PngEncode(contact.ProfilePicture) + '" width="200" height="200">' ;
      }

      return '<li>' + 
                  '<details>' + 
                        '<summary>' + contact.Secondname + ' ' + contact.Firstname + '</summary>' +
                        '<div>' + 
                              htmlImage + 
                              '<p>Born: ' + contact.DateOfBirth + '</p>' +
                              '<p>Phone: ' + contact.Phonenumber + '</p>' +
                              '<p>From: ' + contact.Country + ': ' + contact.City + ', ' + contact.Streetname + ' ' + contact.Housenumber + '</p>' +
                        '</div>' + 
                  '</details>' + 
            '</li>' + '<hr>';
}

function ImageUpload(evt) {
      var image = evt.target.files[0];
  
      var reader = new FileReader();

      var imageData = new Object();
      // Auslesen der Datei-Metadaten
      imageData.name = image.name;
      imageData.date = image.lastModified;
      imageData.size = image.size;
      imageData.type = image.type;
  
      // Wenn der Dateiinhalt ausgelesen wurde...
      reader.onload = function(theFileData) {
        senddata.fileData = theFileData.target.result;
      }
  
      // Die Datei einlesen und in eine Data-URL konvertieren
      reader.readAsDataURL(uploadDatei);
}

async function SubmitContactInfo()
{
      var contactInfo = new class
      {
            Firstname = document.getElementById('firstname').value; 
            Secondname = document.getElementById('secondname').value;
            DateOfBirth = document.getElementById('dateOfBirthDay').value + '.' + document.getElementById('dateOfBirthMonth').value + '.' + document.getElementById('dateOfBirthYear').value;
            Phonenumber = document.getElementById('phonenumber').value;
            ProfilePicture = document.getElementById('profilepicture').value;
            Country = document.getElementById('country').value;
            City = document.getElementById('city').value;
            Streetname = document.getElementById('streetname').value;
            Housenumber = document.getElementById('housenumber').value;
      };

      PostContactInfo('https://localhost:44373/api/contactinfo', contactInfo);
}

async function GetContactInfo(apiPath)
{
      return await fetch(apiPath, {
            mode: 'cors',
            method: "GET"
      })
      .then(res => {
            return res.json();
      });
}

async function PostContactInfo(apiPath, contactInfo)
{
      return await fetch(apiPath, {
            mode: 'cors',
            method: 'POST',
            headers: {
                  'Content-Type': 'application/json'
            },
            body: JSON.stringify(contactInfo)
      })
      .then(data =>{
            return data.json();
      });
}

function Base64PngEncode(x) {
      return "data:image/png;base64, " + x;
};

