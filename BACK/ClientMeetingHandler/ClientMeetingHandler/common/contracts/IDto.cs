using System.Text.Json.Serialization;
using ClientMeetingHandler.presentation.dto;
using ClientMeetingHandler.presentation.dtos;

namespace ClientMeetingHandler.common.contracts;

[JsonDerivedType(typeof(ClientDto), "client")]
[JsonDerivedType(typeof(ContactDto), "contact")]
[JsonDerivedType(typeof(LocalizationDto), "localization")]
[JsonDerivedType(typeof(MeetingDto), "meeting")]
[JsonDerivedType(typeof(NoteDto), "note")]
[JsonDerivedType(typeof(ServiceDto), "service")]
[JsonDerivedType(typeof(ServiceTypeDto), "service_type")]
public interface IDto {}