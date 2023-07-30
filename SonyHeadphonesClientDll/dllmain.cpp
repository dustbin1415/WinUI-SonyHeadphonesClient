﻿// dllmain.cpp : 定义 DLL 应用程序的入口点。
#include "pch.h"

#include <stdio.h>
#include <memory>
#include "WindowsBluetoothConnector.h"
#include "CommandSerializer.h"
#include "BluetoothWrapper.h"
#include "Headphones.h"

void BluetoothWrapper::real_disconnect() noexcept
{
	this->disconnect();
	this->_addr.clear();
}

bool BluetoothWrapper::refresh() noexcept
{
	this->disconnect();
	Sleep(500);
	bool success = this->connect(_addr);
	Sleep(500);
	return success;
}

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
	__declspec(dllexport) void GetDevices(Devices _devices)
	{
		wrap = _bt.getConnectedDevices();
		for (int index = 0; index < wrap.size(); index ++)
		{
			strcpy_s(_devices.device[index].name, wrap[index].name.c_str());
			strcpy_s(_devices.device[index].mac, wrap[index].mac.c_str());
		}
		return;
	}

	__declspec(dllexport) bool IsConnected()
	{
		return _bt.isConnected();
	}

	__declspec(dllexport) bool ConnectDevice(int val)
	{
		return _bt.connect(wrap[val].mac);
	}

	__declspec(dllexport) void DisConnectDevice()
	{
		_bt.real_disconnect();
		return;
	}

	__declspec(dllexport) bool IsSetAsmLevelAvailable()
	{
		return _headphone.isSetAsmLevelAvailable();
	}

	__declspec(dllexport) int GetAsmLevel()
	{
		return _headphone.getAsmLevel();
	}

	__declspec(dllexport) void SetAmbientSoundControl(bool val)
	{
		_headphone.setAmbientSoundControl(val);
	}

	__declspec(dllexport) bool GetAmbientSoundControl()
	{
		return _headphone.getAmbientSoundControl();
	}

	__declspec(dllexport) void SetAsmLevel(int val)
	{
		_headphone.setAsmLevel(val);
		return;
	}

	__declspec(dllexport) bool IsFocusOnVoiceAvailable()
	{
		return _headphone.isFocusOnVoiceAvailable();
	}

	__declspec(dllexport) void SetFocusOnVoice(bool val)
	{
		_headphone.setFocusOnVoice(val);
		return;
	}

	__declspec(dllexport) bool GetFocusOnVoice()
	{
		return _headphone.getFocusOnVoice();
	}

	__declspec(dllexport) bool IsChanged()
	{
		return _headphone.isChanged();
	}

	__declspec(dllexport) bool SetChanges()
	{
		bool tmp = _headphone.setChanges();
		return tmp;
	}
}