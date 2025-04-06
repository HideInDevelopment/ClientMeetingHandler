using System.Text.Json.Serialization;
using ClientMeetingHandler.presentation.dto;
using ClientMeetingHandler.presentation.dto.client;
using ClientMeetingHandler.presentation.dto.contact;
using ClientMeetingHandler.presentation.dto.location;
using ClientMeetingHandler.presentation.dto.meeting;
using ClientMeetingHandler.presentation.dto.note;
using ClientMeetingHandler.presentation.dto.service;
using ClientMeetingHandler.presentation.dto.serviceType;

namespace ClientMeetingHandler.common.contracts;

[JsonDerivedType(typeof(ClientDto), "client")]
[JsonDerivedType(typeof(ContactDto), "contact")]
[JsonDerivedType(typeof(LocationDto), "Location")]
[JsonDerivedType(typeof(MeetingDto), "meeting")]
[JsonDerivedType(typeof(NoteDto), "note")]
[JsonDerivedType(typeof(ServiceDto), "service")]
[JsonDerivedType(typeof(ServiceTypeDto), "service_type")]
public interface IDto {}