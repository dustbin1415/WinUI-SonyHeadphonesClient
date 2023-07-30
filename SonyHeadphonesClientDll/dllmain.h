#pragma once

#define WIN32_LEAN_AND_MEAN 
#include <windows.h>
#include <stdio.h>
#include <memory>
#include "WindowsBluetoothConnector.h"
#include "CommandSerializer.h"
#include "BluetoothWrapper.h"
#include "Headphones.h"

std::unique_ptr<IBluetoothConnector> connector = std::make_unique<WindowsBluetoothConnector>();
BluetoothWrapper _bt(std::move(connector));
Headphones _headphone(_bt);
std::vector<BluetoothDevice> wrap;

struct Devices
{
	struct Device
	{
		char name[100];
		char mac[18];
	};
	Device device[10];
};

extern "C"
{
	__declspec(dllexport) void GetDevices(Devices _devices);
	__declspec(dllexport) bool IsConnected();
	__declspec(dllexport) bool ConnectDevice(int val);
	__declspec(dllexport) void DisConnectDevice();
	__declspec(dllexport) bool IsSetAsmLevelAvailable();
	__declspec(dllexport) int GetAsmLevel();
	__declspec(dllexport) void SetAmbientSoundControl(bool val);
	__declspec(dllexport) bool GetAmbientSoundControl();
	__declspec(dllexport) void SetAsmLevel(int val);
	__declspec(dllexport) bool IsFocusOnVoiceAvailable();
	__declspec(dllexport) void SetFocusOnVoice(bool val);
	__declspec(dllexport) bool GetFocusOnVoice();
	__declspec(dllexport) bool IsChanged();
	__declspec(dllexport) bool SetChanges();
}