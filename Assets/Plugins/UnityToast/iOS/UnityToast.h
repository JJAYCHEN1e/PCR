//
//  UnityToast.h
//  UnityToast
//
//  Created by jjaychen on 2021/7/2.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

NS_ASSUME_NONNULL_BEGIN

@interface UnityToast : NSObject
+(void)initBotEmojiView;
+(void)showBotToast:(NSString *)string with:(float)duration;

+(void)showTopToast:(NSString *)string with:(float)duration;
+(void)showBottomToast:(NSString *)string with:(float)duration;

+(void)showAlert:(NSString *)title message:(NSString *)message;
@end

NS_ASSUME_NONNULL_END
