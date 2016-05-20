# hotel-api-sdk-Net

## Introduction 
Hotelbeds SDK for NET is a set of utilities whose main goal is to help in the development of NET applications that use APItude, the Hotelbeds API.
This is a nuget package available on nuget.org repository. 

https://www.nuget.org/packages/hotel-api-sdk-net/0.1.0

## License
This softwared is licensed under the LGPL v2.1 license. Please refer to the file LICENSE for specific details and more license and copyright information.

## Install
Install from console with Package Manager Console:

```bash
Install-Package hotel-api-sdk-net
```

## Using SDK

### Overview

The HotelApiClient class has different methods that implement the various calls HotelAPI:

* doAvailability
* doCheck
* confirm
* Cancel
* Detail
* List
* Status

Within the project are the helpers which allow you to create different objects well formed request required by the Rest API Hotelbeds:

* Availability
* BookingCheck
* Booking


### Example of simple availability

```c#

HotelApiClient client = new HotelApiClient();
StatusRS status = client.status();

if (status != null && status.error == null)
	Console.WriteLine("StatusRS: " + status.status);
else if (status != null && status.error != null)
{
	Console.WriteLine("StatusRS: " + status.status + " " + status.error.code + ": " + status.error.message);
	return;
}
else if (status == null)
{
	Console.WriteLine("StatusRS: Is not available.");
	return;
}

Availability avail = new Availability();
avail.checkIn = DateTime.Now.AddDays(10);
avail.checkOut = DateTime.Now.AddDays(13);
avail.withinThis = new Availability.Circle() {
	latitude = 13.752474,
	longitude = 100.4657878,
	radiusInKilometers = 50
	};
AvailRoom room = new AvailRoom();
room.adults = 1;
room.details = new List<RoomDetail>();
room.adultOf(30);
room.numberOfRooms = 1;
avail.rooms.Add(room);

AvailabilityRQ availabilityRQ = avail.toAvailabilityRQ();
if (availabilityRQ == null)
    throw new Exception("Availability RQ can't be null", new ArgumentNullException());

AvailabilityRS responseAvail = client.doAvailability(availabilityRQ);

if (responseAvail != null && responseAvail.hotels != null && responseAvail.hotels.hotels != null && responseAvail.hotels.hotels.Count > 0)
{
foreach (var hotel in responseAvail.hotels.hotels)
        Console.WriteLine(String.Format("Hotel Name: {0} Category Name: {1} Destination: {2} Rooms: {3}", hotel.name, hotel.categoryName, hotel.destinationName, hotel.rooms.Count));
}

```
