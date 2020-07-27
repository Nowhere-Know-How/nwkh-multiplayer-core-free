local nk = require("nakama")

local function create_group(context, payload)
    local metadata = { -- Add whatever custom fields you want.
        -- my_custom_field = "some value"
    }
    local user_id = "0cd1b2f5-4f30-44cc-9793-cfac3be8b0ef" -- nowhereknowhow admin account
    local name = "players-online-group"
    local creator_id = user_id
    local lang = "en_US"
    local description = "list of all players online"
    local avatar_url = "url://somelink"
    local open = true
    local maxMemberCount = 100

    local success, err = pcall(nk.group_create, user_id, name, creator_id, lang,
        description, avatar_url, open, metadata, maxMemberCount)
    if (not success) then
        nk.logger_error(("Could not create group: %q"):format(err))
        return nk.json_encode(err)
    end
    return nk.json_encode({success})
end
nk.register_rpc(create_group, "create_group_rpc")


local function delete_group(context, payload)
    local metadata = { -- Add whatever custom fields you want.
        -- my_custom_field = "some value"
    }
    local user_id = "a545911c-1a5d-46cb-b6d8-379114f2f193"
    local name = "players-online-group"
    local creator_id = user_id
    local lang = "en_US"
    local description = "list of all players online"
    local avatar_url = "url://somelink"
    local open = true
    local maxMemberCount = 100

    local success, err = pcall(nk.group_create, user_id, name, creator_id, lang,
        description, avatar_url, open, metadata, maxMemberCount)
    if (not success) then
        nk.logger_error(("Could not create group: %q"):format(err))
        return nk.json_encode(err)
    end
    return nk.json_encode({success})
end
nk.register_rpc(delete_group, "delete_group_rpc")